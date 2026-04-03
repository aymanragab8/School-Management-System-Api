using AutoMapper;

namespace SchoolProject.Application.Mapping.Courses
{
    public partial class CourseMapping : Profile
    {
        public CourseMapping()
        {
            GetCourseListMapping();
            GetCourseByIdMapping();
            GetCoursesByGradeIdMapping();
            AddCourseMapping();
            UpdateCourseMapping();
        }
    }
}
