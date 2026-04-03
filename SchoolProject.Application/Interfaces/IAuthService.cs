using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Auth.Commands.Models;
using SchoolProject.Application.Features.Auth.Responses;

namespace SchoolProject.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Response<RegisterResponse>> RegisterAsync(RegisterCommand request);
        Task<Response<AuthResponse>> LoginAsync(LoginCommand request);
        Task<Response<AuthResponse>> RefreshTokenAsync(RefreshTokenCommand request);
    }
}