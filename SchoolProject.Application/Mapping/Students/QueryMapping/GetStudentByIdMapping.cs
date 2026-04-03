using SchoolProject.Application.Features.Students.Queries.Response;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Mapping.Students
{
    public partial class StudentMapping
    {
        public void GetStudentByIdMapping()
        {
            CreateMap<Student, GetStudentByIdResponse>()
                 .ForMember(dest => dest.GradeName,
                          opt => opt.MapFrom(src => src.Grade.Name));

            CreateMap<Enrollment, EnrollmentResponse>()
                .ForMember(dest => dest.CourseName,
                          opt => opt.MapFrom(src => src.Course.Name));

        }
    }
}
