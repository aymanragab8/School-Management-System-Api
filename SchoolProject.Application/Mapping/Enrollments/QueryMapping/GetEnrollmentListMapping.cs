using SchoolProject.Application.Features.Enrollments.Queries.Response;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Mapping.Enrollments
{
    public partial class EnrollmentMapping
    {
        public void GetEnrollmentListMapping()
        {
            CreateMap<Enrollment, GetEnrollmentListResponse>();

        }
    }
}

