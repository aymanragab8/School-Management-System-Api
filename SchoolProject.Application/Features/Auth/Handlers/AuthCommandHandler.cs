using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Auth.Commands.Models;
using SchoolProject.Application.Features.Auth.Responses;
using SchoolProject.Application.Interfaces;

namespace SchoolProject.Application.Features.Auth.Commands.Handlers
{
    public class AuthCommandHandler : ResponseHandler,
        IRequestHandler<RegisterCommand, Response<RegisterResponse>>,
        IRequestHandler<LoginCommand, Response<AuthResponse>>,
        IRequestHandler<RefreshTokenCommand, Response<AuthResponse>>
    {
        private readonly IAuthService _authService;

        public AuthCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Response<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterAsync(request);
        }

        public async Task<Response<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(request);
        }

        public async Task<Response<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RefreshTokenAsync(request);
        }
    }
}