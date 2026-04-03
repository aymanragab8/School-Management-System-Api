using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Students.Queries.Response;

namespace SchoolProject.Application.Features.Students.Queries.Models
{
    public class GetStudentListQuery : IRequest<Response<PaginatedResponse<GetStudentListResponse>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public GetStudentListQuery(int PageNumber, int PageSize)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
        }
    }
}
