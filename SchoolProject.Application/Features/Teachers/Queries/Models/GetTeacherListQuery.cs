using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Teachers.Queries.Response;

namespace SchoolProject.Application.Features.Teachers.Queries.Models
{
    public class GetTeacherListQuery : IRequest<Response<PaginatedResponse<GetTeacherListResponse>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public GetTeacherListQuery(int PageNumber, int PageSize)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
        }
    }
}
