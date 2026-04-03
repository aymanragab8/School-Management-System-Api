using SchoolProject.Application.Features.Grades.Queries.Response;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Application.Mapping.Grades
{
    public partial class GradeMapping
    {
        public void GetGradeByIdMapping()
        {
            CreateMap<Grade, GetGradeByIdResponse>();
        }
    }
}
