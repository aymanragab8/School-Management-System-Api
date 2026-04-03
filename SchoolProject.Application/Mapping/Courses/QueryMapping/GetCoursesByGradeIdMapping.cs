using SchoolProject.Application.Features.Courses.Queries.Response;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Mapping.Courses
{
    public partial class CourseMapping
    {
        public void GetCoursesByGradeIdMapping()
        {
            CreateMap<Course, GetCoursesByGradeIdResponse>();
        }
    }
}
