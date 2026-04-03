using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Enrollments.Queries.Response;
using System.Text.Json.Serialization;

namespace SchoolProject.Application.Features.Enrollments.Queries.Models
{
    public class GetEnrollmentsByCourseIdQuery : IRequest<Response<PaginatedResponse<GetEnrollmentsByCourseIdResponse>>>
    {
        public int CourseId { get; set; }

        [JsonIgnore]
        public string? currentUserId { get; set; }
        [JsonIgnore]
        public string? role { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public GetEnrollmentsByCourseIdQuery(int courseId, int PageNumber, int PageSize)
        {
            CourseId = courseId;
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
        }
    }
}