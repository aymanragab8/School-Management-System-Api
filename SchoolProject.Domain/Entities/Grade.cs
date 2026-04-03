namespace SchoolProject.Domain.Entities
{
    public class Grade : BaseEntity
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public ICollection<Student>? Students { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }
}
