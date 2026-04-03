namespace SchoolProject.Domain.Entities
{
    public class Student : Person
    {
        public ICollection<Enrollment>? Enrollments { get; set; } = new List<Enrollment>();
        public Grade? Grade { get; set; }
        public int? GradeId { get; set; }
    }
}
