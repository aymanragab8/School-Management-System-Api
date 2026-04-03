using SchoolProject.Application.Features.Teachers.Commands.Models;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Mapping.Teachers
{
    public partial class TeacherMapping
    {
        public void AddTeacherMappings()
        {
            CreateMap<AddTeacherCommand, Teacher>();
        }
    }
}
