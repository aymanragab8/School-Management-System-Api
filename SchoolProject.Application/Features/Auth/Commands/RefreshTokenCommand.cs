using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Auth.Responses;

namespace SchoolProject.Application.Features.Auth.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<AuthResponse>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}