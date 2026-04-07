using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Application.Features.Auth.Commands.Models;
using SchoolProject.Domain.AppMeteData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class AuthController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Router.Auth.StudentRegister)]

        public async Task<IActionResult> StudentRegister([FromBody] RegisterStudentCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }
        [HttpPost(Router.Auth.TeacherRegister)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> TeacherRegister([FromBody] RegisterTeacherCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }
        [HttpPost(Router.Auth.Login)]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }
        [HttpPost(Router.Auth.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }
    }
}
