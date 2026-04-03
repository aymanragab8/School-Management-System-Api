using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Grades.Commands.Models
{
    public class DeleteGradeCommand : IRequest<Response<string>>
    {
        public int gradeId { get; set; }
        public DeleteGradeCommand(int gradeid)
        {
            gradeId = gradeid;
        }
    }
}
