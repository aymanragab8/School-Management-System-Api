using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Auth.Commands.Models;
using SchoolProject.Application.Features.Auth.Responses;
using SchoolProject.Application.Interfaces;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Helpers;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastruture.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Infrastructure.Services
{
    public class AuthService : ResponseHandler, IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStudentRepository _studentRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ILogger<AuthService> _logger;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IStudentRepository studentRepository,
            IGradeRepository gradeRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IOptions<JwtSettings> jwtSettings,
                ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _studentRepository = studentRepository;
            _gradeRepository = gradeRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _logger = logger;
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<Response<RegisterResponse>> RegisterAsync(RegisterCommand request)
        {
            _logger.LogInformation("Register attempt for {Email}", request.Email);

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("Register failed - Email {Email} already exists", request.Email);
                return BadRequest<RegisterResponse>("Email is already registered");
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                _logger.LogError("Register failed for {Email}: {Errors}",
                    request.Email,
                    string.Join(", ", createResult.Errors.Select(e => e.Description)));

                return BadRequest<RegisterResponse>(
                    string.Join(", ", createResult.Errors.Select(e => e.Description)));
            }

            await _userManager.AddToRoleAsync(user, "Admin");

            var student = new Student
            {
                ApplicationUserId = user.Id,
                FullName = request.FullName,
                NationalId = request.NationalId,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                GradeId = request.GradeId
            };

            if (request.GradeId.HasValue)
            {
                var gradeExists = await _gradeRepository.GetByIdAsync(request.GradeId.Value);
                if (gradeExists == null)
                {
                    _logger.LogWarning("Register failed - Grade {GradeId} not found", request.GradeId);
                    return BadRequest<RegisterResponse>("Grade not found");
                }
            }

            await _studentRepository.AddAsync(student);

            _logger.LogInformation("Register successful for {Email} - StudentId created", request.Email);

            return Registered<RegisterResponse>();
        }
        public async Task<Response<AuthResponse>> LoginAsync(LoginCommand request)
        {
            _logger.LogInformation("Login attempt for {Email}", request.Email);
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                _logger.LogWarning("Login failed for {Email} - user not found", request.Email);
                return BadRequest<AuthResponse>("Email or password is incorrect");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
                return BadRequest<AuthResponse>("Email or password is incorrect");

            _logger.LogInformation("Login successful for {Email}", request.Email);
            var tokens = await GenerateTokensAsync(user);
            return Success(tokens);
        }
        public async Task<Response<AuthResponse>> RefreshTokenAsync(RefreshTokenCommand request)
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null)
                return BadRequest<AuthResponse>("Invalid access token");

            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId!);
            if (user == null)
                return NotFound<AuthResponse>("User not found");

            var refreshToken = await _refreshTokenRepository
                .GetByTokenAsync(request.RefreshToken);

            if (refreshToken == null || refreshToken.IsRevoked || refreshToken.ExpiresAt < DateTime.UtcNow)
                return BadRequest<AuthResponse>("Invalid or expired refresh token");

            refreshToken.IsRevoked = true;
            await _refreshTokenRepository.UpdateAsync(refreshToken);

            var tokens = await GenerateTokensAsync(user);
            return Success(tokens);
        }
        private async Task<AuthResponse> GenerateTokensAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Email, user.Email!),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var accessTokenExpiry = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpireMinutes);

            var accessToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: accessTokenExpiry,
                signingCredentials: creds
            );

            var refreshTokenValue = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDays);

            var refreshToken = new RefreshToken
            {
                ApplicationUserId = user.Id,
                Token = refreshTokenValue,
                ExpiresAt = refreshTokenExpiry,
                IsRevoked = false
            };
            await _refreshTokenRepository.AddAsync(refreshToken);

            return new AuthResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = refreshTokenValue,
                AccessTokenExpiresAt = accessTokenExpiry,
                RefreshTokenExpiresAt = refreshTokenExpiry,
                Email = user.Email!,
                Roles = roles.ToList()
            };
        }
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Key))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(
                token, tokenValidationParameters, out _);

            return principal;
        }
    }
}