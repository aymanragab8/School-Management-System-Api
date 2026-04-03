using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Grades.Queries.Response;

namespace SchoolProject.Application.Features.Grades.Queries.Models
{
    public class GetGradeListQuery : IRequest<Response<PaginatedResponse<GetGradeListResponse>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public GetGradeListQuery(int PageNumber, int PageSize)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
        }
    }
}
