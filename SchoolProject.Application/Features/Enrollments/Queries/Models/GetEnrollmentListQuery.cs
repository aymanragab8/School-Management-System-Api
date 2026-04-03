using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Enrollments.Queries.Response;

namespace SchoolProject.Application.Features.Enrollments.Queries.Models
{
    public class GetEnrollmentListQuery : IRequest<Response<PaginatedResponse<GetEnrollmentListResponse>>>
    {
        public string role { get; set; }
        public string currentUserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public GetEnrollmentListQuery(int PageNumber, int PageSize)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
        }
    }
}
