using AutoMapper;

namespace SchoolProject.Application.Mapping.Teachers
{
    public partial class TeacherMapping : Profile
    {
        public TeacherMapping()
        {
            GetTeacherListMapping();
            GetTeacherByIdMapping();
            AddTeacherMappings();
            UpdateTeacherMappings();
        }
    }
}
