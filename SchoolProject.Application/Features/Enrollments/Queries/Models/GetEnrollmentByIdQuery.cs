using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Enrollments.Queries.Response;

namespace SchoolProject.Application.Features.Enrollments.Queries.Models
{
    public class GetEnrollmentByIdQuery : IRequest<Response<GetEnrollmentByIdResponse>>
    {
        public int enrollmentId { get; set; }
        public GetEnrollmentByIdQuery(int enrollmentId)
        {
            enrollmentId = enrollmentId;
        }
    }
}
