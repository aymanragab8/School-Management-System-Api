using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Courses.Queries.Response;

namespace SchoolProject.Application.Features.Courses.Queries.Models
{
    public class GetCourseListQuery : IRequest<Response<PaginatedResponse<GetCourseListResponse>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public GetCourseListQuery(int PageNumber, int PageSize)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
        }
    }
}
