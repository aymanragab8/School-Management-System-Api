using SchoolProject.Application.Features.Enrollments.Commands.Models;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Mapping.Enrollments

{
    public partial class EnrollmentMapping
    {
        public void UpdateEnrollmentMappings()
        {
            CreateMap<UpdateEnrollmentCommand, Enrollment>();
        }
    }
}
