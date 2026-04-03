using SchoolProject.Application.Features.Students.Queries.Response;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Mapping.Students
{
    public partial class StudentMapping
    {
        public void GetStudentListMapping()
        {
            CreateMap<Student, GetStudentListResponse>()
                   .ForMember(dest => dest.GradeName,
                         opt => opt.MapFrom(src => src.Grade.Name));
        }
    }
}
