using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Application.Features.Enrollments.Commands.Models;
using SchoolProject.Application.Features.Enrollments.Queries.Models;
using SchoolProject.Domain.AppMeteData;
using System.Security.Claims;

namespace SchoolProject.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class EnrollmentController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public EnrollmentController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Router.Enrollment.GetAll)]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> GetAllEnrollments([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetEnrollmentListQuery(pageNumber, pageSize)
            {
                currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                role = User.FindFirstValue(ClaimTypes.Role)
            };
            var enrollments = await _mediator.Send(query);
            return NewResult(enrollments);
        }
        [HttpGet(Router.Enrollment.GetAllByCourse)]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetAllEnrollmentsByCourse([FromQuery] int courseId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetEnrollmentsByCourseIdQuery(courseId, pageNumber, pageSize)
            {
                currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                role = User.FindFirstValue(ClaimTypes.Role)
            };
            var enrollments = await _mediator.Send(query);
            return NewResult(enrollments);
        }

        [HttpGet(Router.Enrollment.GetById)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSingleEnrollment([FromRoute] int enrollmentId)
        {
            var enrollment = await _mediator.Send(new GetEnrollmentByIdQuery(enrollmentId));
            return NewResult(enrollment);
        }
        [HttpPost(Router.Enrollment.Create)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateNewEnrollment([FromBody] AddEnrollmentCommand enrollment)
        {
            var result = await _mediator.Send(enrollment);
            return NewResult(result);
        }
        [HttpPut(Router.Enrollment.Update)]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateEnrollmentData([FromRoute] int enrollmentId, [FromBody] UpdateEnrollmentCommand command)
        {
            command.Id = enrollmentId;
            var result = await _mediator.Send(command);
            return NewResult(result);
        }
        [HttpDelete(Router.Enrollment.Delete)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEnrollment([FromRoute] int enrollmentId)
        {
            var result = await _mediator.Send(new DeleteEnrollmentCommand(enrollmentId));
            return NewResult(result);
        }
    }
}
