using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Students.Queries.Response;

namespace SchoolProject.Application.Features.Students.Queries.Models
{
    public class GetStudentLitByGradeQuery : IRequest<Response<PaginatedResponse<GetStudentListResponse>>>
    {
        public int gradeId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public GetStudentLitByGradeQuery(int gradeId, int PageNumber, int PageSize)
        {
            this.gradeId = gradeId;
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
        }
    }
}
