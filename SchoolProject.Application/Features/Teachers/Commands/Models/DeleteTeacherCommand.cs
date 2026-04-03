using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Teachers.Commands.Models
{
    public class DeleteTeacherCommand : IRequest<Response<string>>
    {
        public int teacherId { get; set; }
        public DeleteTeacherCommand(int teacherid)
        {
            teacherId = teacherid;
        }
    }
}
