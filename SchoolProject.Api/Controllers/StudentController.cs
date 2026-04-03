using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Application.Features.Students.Commands.Models;
using SchoolProject.Application.Features.Students.Queries.Models;
using SchoolProject.Domain.AppMeteData;
using System.Security.Claims;

namespace SchoolProject.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class StudentController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }
        [HttpGet(Router.Student.GetAll)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllStudents([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var students = await _mediator.Send(new GetStudentListQuery(pageNumber, pageSize));
            return NewResult(students);
        }
        [HttpGet(Router.Student.GetByGrade)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllStudentsByGrade([FromRoute] int gradeId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var students = await _mediator.Send(new GetStudentLitByGradeQuery(gradeId, pageNumber, pageSize));
            return NewResult(students);
        }
        [HttpGet(Router.Student.GetById)]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> GetSingleStudent([FromRoute] int studentId)
        {
            var query = new GetStudentByIdQuery(studentId)
            {
                currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                role = User.FindFirst(ClaimTypes.Role)?.Value
            };
            var student = await _mediator.Send(query);
            return NewResult(student);
        }
        [HttpPost(Router.Student.Create)]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> CreateNewStudent(AddStudentCommand student)
        {
            var result = await _mediator.Send(student);
            return NewResult(result);
        }
        [HttpPut(Router.Student.Update)]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> UpdateStudentData([FromRoute] int studentId, [FromBody] UpdateStudentCommand command)
        {
            command.StudentId = studentId;
            command.currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            command.role = User.FindFirstValue(ClaimTypes.Role);

            var result = await _mediator.Send(command);
            return NewResult(result);
        }
        [HttpDelete(Router.Student.Delete)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int studentId)
        {
            var result = await _mediator.Send(new DeleteStudentCommand(studentId));
            return NewResult(result);
        }
    }
}