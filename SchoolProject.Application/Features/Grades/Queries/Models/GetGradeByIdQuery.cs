using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Grades.Queries.Response;

namespace SchoolProject.Application.Features.Grades.Queries.Models
{
    public class GetGradeByIdQuery : IRequest<Response<GetGradeByIdResponse>>
    {
        public int GradeId { get; set; }
        public GetGradeByIdQuery(int gradeId)
        {
            this.GradeId = gradeId;
        }
    }
}
