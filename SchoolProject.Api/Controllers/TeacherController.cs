using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Application.Features.Teachers.Commands.Models;
using SchoolProject.Application.Features.Teachers.Queries.Models;
using SchoolProject.Domain.AppMeteData;
using System.Security.Claims;


namespace SchoolProject.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class TeacherController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public TeacherController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Router.Teacher.GetAll)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTeachers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var teachers = await _mediator.Send(new GetTeacherListQuery(pageNumber, pageSize));
            return NewResult(teachers);
        }

        [HttpGet(Router.Teacher.GetById)]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetSingleTeacher([FromRoute] int teacherId)
        {
            var query = new GetTeacherByIdQuery(teacherId)
            {
                currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                role = User.FindFirstValue(ClaimTypes.Role)

            };
            var teacher = await _mediator.Send(query);
            return NewResult(teacher);
        }
        [HttpPost(Router.Teacher.Create)]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> CreateNewTeacher(AddTeacherCommand teacher)
        {
            var result = await _mediator.Send(teacher);
            return NewResult(result);
        }
        [HttpPut(Router.Teacher.Update)]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateTeacherData([FromRoute] int teacherId, [FromBody] UpdateTeacherCommand command)
        {
            command.TeacherId = teacherId;
            command.currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            command.role = User.FindFirstValue(ClaimTypes.Role);

            var result = await _mediator.Send(command);
            return NewResult(result);
        }
        [HttpDelete(Router.Teacher.Delete)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTeacher([FromRoute] int teacherId)
        {
            var result = await _mediator.Send(new DeleteTeacherCommand(teacherId));
            return NewResult(result);
        }

    }
}


