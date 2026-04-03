namespace SchoolProject.Application.Features.Grades.Queries.Response
{
    public class GetGradeListResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
