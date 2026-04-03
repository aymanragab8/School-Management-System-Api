using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Auth.Responses;

namespace SchoolProject.Application.Features.Auth.Commands.Models
{
    public class LoginCommand : IRequest<Response<AuthResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}