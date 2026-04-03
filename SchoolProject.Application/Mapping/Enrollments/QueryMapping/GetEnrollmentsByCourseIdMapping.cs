using SchoolProject.Application.Features.Enrollments.Queries.Response;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Mapping.Enrollments
{
    public partial class EnrollmentMapping
    {
        public void GetEnrollmentsByCourseIdMapping()
        {
            CreateMap<Enrollment, GetEnrollmentsByCourseIdResponse>()
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FullName))
            .ForMember(dest => dest.StudentPhone, opt => opt.MapFrom(src => src.Student.PhoneNumber))
            .ForMember(dest => dest.StudentAddress, opt => opt.MapFrom(src => src.Student.Address));

        }
    }
}

