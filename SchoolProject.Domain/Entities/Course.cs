namespace SchoolProject.Domain.Entities
{
    public class Course : BaseEntity
    {
        public int TeacherId { get; set; }
        public int GradeId { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public bool IsActive { get; set; } = true;
        public Teacher? Teacher { get; set; }
        public Grade? Grade { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; } = new List<Enrollment>();
    }
}
