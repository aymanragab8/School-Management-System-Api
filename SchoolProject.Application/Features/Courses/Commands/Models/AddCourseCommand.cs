using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Courses.Commands.Models
{
    public class AddCourseCommand : IRequest<Response<string>>
    {
        public int TeacherId { get; set; }
        public int GradeId { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
    }
}
