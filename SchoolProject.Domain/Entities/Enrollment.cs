using SchoolProject.Domain.Enums;

namespace SchoolProject.Domain.Entities
{
    public class Enrollment : BaseEntity
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;
        public float? Score { get; set; }
        public Student? Student { get; set; }
        public Course? Course { get; set; }
    }
}
