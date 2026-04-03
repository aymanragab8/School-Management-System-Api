using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Application.Features.Courses.Commands.Models;
using SchoolProject.Application.Features.Courses.Queries.Models;
using SchoolProject.Domain.AppMeteData;
using System.Security.Claims;

namespace SchoolProject.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class CourseController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet(Router.Course.GetAll)]
        public async Task<IActionResult> GetAllCourses([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var courses = await _mediator.Send(new GetCourseListQuery(pageNumber, pageSize));
            return NewResult(courses);
        }
        [HttpGet(Router.Course.GetAllByGrade)]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> GetAllCoursesByGrade([FromQuery] int gradeId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetCoursesByGradeIdQuery(gradeId, pageNumber, pageSize)
            {
                currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                role = User.FindFirstValue(ClaimTypes.Role)
            };
            var courses = await _mediator.Send(query);
            return NewResult(courses);
        }
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet(Router.Course.GetById)]
        public async Task<IActionResult> GetSingleCourse([FromRoute] int courseId)
        {
            var query = new GetCourseByIdQuery(courseId)
            {
                currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                role = User.FindFirstValue(ClaimTypes.Role)
            };
            var course = await _mediator.Send(query);
            return NewResult(course);
        }
        [HttpPost(Router.Course.Create)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateNewCourse([FromBody] AddCourseCommand course)
        {
            var result = await _mediator.Send(course);
            return NewResult(result);
        }
        [HttpPut(Router.Course.Update)]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateCourseData([FromRoute] int courseId, [FromBody] UpdateCourseCommand command)
        {
            command.Id = courseId;
            command.currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            command.role = User.FindFirstValue(ClaimTypes.Role);
            var result = await _mediator.Send(command);
            return NewResult(result);
        }
        [HttpDelete(Router.Course.Delete)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int courseId)
        {
            var result = await _mediator.Send(new DeleteCourseCommand(courseId));
            return NewResult(result);
        }


    }
}
