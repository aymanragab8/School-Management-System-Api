namespace SchoolProject.Application.Features.Courses.Queries.Response
{
    public class GetCoursesByGradeIdResponse
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int GradeId { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
