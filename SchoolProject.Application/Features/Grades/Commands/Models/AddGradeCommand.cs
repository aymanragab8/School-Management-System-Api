using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Grades.Commands.Models
{
    public class AddGradeCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public int Level { get; set; }
    }
}
