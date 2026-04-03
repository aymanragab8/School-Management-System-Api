using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Students.Commands.Models
{
    public class DeleteStudentCommand : IRequest<Response<string>>
    {
        public int studentId { get; set; }
        public DeleteStudentCommand(int studentId)
        {
            this.studentId = studentId;
        }
    }
}
