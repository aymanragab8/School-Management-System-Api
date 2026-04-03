namespace SchoolProject.Domain.Entities
{
    public class Teacher : Person
    {
        public ICollection<Course>? Courses { get; set; } = new List<Course>();
        public string Specialization { get; set; }
        public decimal Salary { get; set; }
    }
}
