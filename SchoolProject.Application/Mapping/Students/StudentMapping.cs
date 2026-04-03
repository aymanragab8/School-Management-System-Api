using AutoMapper;

namespace SchoolProject.Application.Mapping.Students
{
    public partial class StudentMapping : Profile
    {
        public StudentMapping()
        {
            GetStudentListMapping();
            GetStudentByIdMapping();
            AddStudentMapping();
            UpdateStudentMapping();
        }
    }
}
