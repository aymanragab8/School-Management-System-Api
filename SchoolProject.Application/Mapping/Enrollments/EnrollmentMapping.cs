using AutoMapper;

namespace SchoolProject.Application.Mapping.Enrollments
{
    public partial class EnrollmentMapping : Profile
    {
        public EnrollmentMapping()
        {
            GetEnrollmentListMapping();
            GetEnrollmentByIdMapping();
            GetEnrollmentsByCourseIdMapping();
            AddEnrollmentMappings();
            UpdateEnrollmentMappings();
        }
    }
}
