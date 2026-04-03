using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Courses.Commands.Models
{
    public class DeleteCourseCommand : IRequest<Response<string>>
    {
        public int courseId { get; set; }
        public DeleteCourseCommand(int courseid)
        {
            courseId = courseid;
        }
    }
}
