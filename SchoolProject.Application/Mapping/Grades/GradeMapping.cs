using AutoMapper;

namespace SchoolProject.Application.Mapping.Grades
{
    public partial class GradeMapping : Profile
    {
        public GradeMapping()
        {
            GetGradeListMapping();
            GetGradeByIdMapping();
            AddGradeMapping();
            UpdateGradeMapping();
        }
    }
}
