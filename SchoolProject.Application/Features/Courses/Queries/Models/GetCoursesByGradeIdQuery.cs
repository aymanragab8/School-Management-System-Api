using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Courses.Queries.Response;

namespace SchoolProject.Application.Features.Courses.Queries.Models
{
    public class GetCoursesByGradeIdQuery : IRequest<Response<PaginatedResponse<GetCoursesByGradeIdResponse>>>
    {
        public int gradeId { get; set; }
        public string role { get; set; }
        public string currentUserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public GetCoursesByGradeIdQuery(int gradeId, int PageNumber, int PageSize)
        {
            this.gradeId = gradeId;
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
        }
    }
}
