using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Courses.Queries.Response;

namespace SchoolProject.Application.Features.Courses.Queries.Models
{
    public class GetCourseByIdQuery : IRequest<Response<GetCourseByIdResponse>>
    {
        public int CourseId { get; set; }
        public string role { get; set; }
        public string currentUserId { get; set; }

        public GetCourseByIdQuery(int courseId)
        {
            this.CourseId = courseId;
        }
    }
}
