using SchoolProject.Application.Features.Courses.Commands.Models;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Mapping.Courses
{
    public partial class CourseMapping
    {
        public void UpdateCourseMapping()
        {
            CreateMap<UpdateCourseCommand, Course>();
        }
    }
}
