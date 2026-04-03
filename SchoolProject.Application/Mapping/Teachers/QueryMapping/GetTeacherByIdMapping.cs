using SchoolProject.Application.Features.Teachers.Queries.Response;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Mapping.Teachers
{
    public partial class TeacherMapping
    {
        public void GetTeacherByIdMapping()
        {
            CreateMap<Teacher, GetTeacherByIdResponse>();
        }
    }
}
