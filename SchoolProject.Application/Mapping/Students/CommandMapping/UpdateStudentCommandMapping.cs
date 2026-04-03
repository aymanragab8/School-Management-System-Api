using SchoolProject.Application.Features.Students.Commands.Models;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Mapping.Students
{
    public partial class StudentMapping
    {
        public void UpdateStudentMapping()
        {
            CreateMap<UpdateStudentCommand, Student>();
        }
    }
}
