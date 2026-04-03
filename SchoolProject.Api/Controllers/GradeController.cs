using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Application.Features.Grades.Commands.Models;
using SchoolProject.Application.Features.Grades.Queries.Models;
using SchoolProject.Domain.AppMeteData;


namespace SchoolProject.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class GradeController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public GradeController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Router.Grade.GetAll)]
        public async Task<IActionResult> GetAllGrades([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var graders = await _mediator.Send(new GetGradeListQuery(pageNumber, pageSize));
            return NewResult(graders);
        }

        [HttpGet(Router.Grade.GetById)]
        public async Task<IActionResult> GetSingleGrade([FromRoute] int gradeId)
        {
            var grade = await _mediator.Send(new GetGradeByIdQuery(gradeId));
            return NewResult(grade);
        }
        [HttpPost(Router.Grade.Create)]
        public async Task<IActionResult> CreateNewGrade(AddGradeCommand grade)
        {
            var result = await _mediator.Send(grade);
            return NewResult(result);
        }
        [HttpPut(Router.Grade.Update)]
        public async Task<IActionResult> UpdateGradeData([FromRoute] int gradeId, [FromBody] UpdateGradeCommand command)
        {
            command.Id = gradeId;
            var result = await _mediator.Send(command);
            return NewResult(result);
        }
        [HttpDelete(Router.Grade.Delete)]
        public async Task<IActionResult> DeleteGrade([FromRoute] int gradeId)
        {
            var result = await _mediator.Send(new DeleteGradeCommand(gradeId));
            return NewResult(result);
        }

    }
}


