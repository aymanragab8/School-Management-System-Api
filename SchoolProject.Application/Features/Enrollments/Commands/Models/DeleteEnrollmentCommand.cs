using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Enrollments.Commands.Models
{
    public class DeleteEnrollmentCommand : IRequest<Response<string>>
    {
        public int enrollmentId { get; set; }
        public DeleteEnrollmentCommand(int enrollmentId)
        {
            this.enrollmentId = enrollmentId;
        }
    }
}
