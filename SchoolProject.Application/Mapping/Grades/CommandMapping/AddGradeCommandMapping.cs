using SchoolProject.Application.Features.Grades.Commands.Models;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Mapping.Grades
{
    public partial class GradeMapping
    {
        public void AddGradeMapping()
        {
            CreateMap<AddGradeCommand, Grade>();
        }
    }
}
