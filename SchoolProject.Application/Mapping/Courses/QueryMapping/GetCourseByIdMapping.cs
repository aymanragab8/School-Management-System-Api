using SchoolProject.Application.Features.Courses.Queries.Response;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Mapping.Courses
{
    public partial class CourseMapping
    {
        public void GetCourseByIdMapping()
        {
            CreateMap<Course, GetCourseByIdResponse>();
        }
    }
}
