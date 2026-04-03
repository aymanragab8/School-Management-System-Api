using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Application.Interfaces;
using SchoolProject.Domain.Helpers;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastructure.Services;
using SchoolProject.Infrastruture.Data;
using SchoolProject.Infrastruture.Identity;
using SchoolProject.Infrastruture.InfrastructureBases;
using SchoolProject.Infrastruture.Repositories;
using SchoolProject.Infrastruture.Repositories.SchoolProject.Infrastructure.Repositories;
using System.Security.Claims;
using System.Text;



namespace SchoolProject.Infrastruture
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IAuthService, AuthService>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("cs")));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer("Bearer", options =>
  {
      options.MapInboundClaims = false;

      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = configuration["JwtSettings:Issuer"],
          ValidAudience = configuration["JwtSettings:Audience"],
          IssuerSigningKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"])),
          ClockSkew = TimeSpan.Zero,
          RoleClaimType = ClaimTypes.Role,
          NameClaimType = ClaimTypes.NameIdentifier
      };
  });
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            return services;
        }

    }
}
