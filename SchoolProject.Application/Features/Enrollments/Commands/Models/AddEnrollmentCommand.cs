using MediatR;
using SchoolProject.Application.Bases;

namespace SchoolProject.Application.Features.Enrollments.Commands.Models
{
    public class AddEnrollmentCommand : IRequest<Response<string>>
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public float? Score { get; set; }
    }
}
