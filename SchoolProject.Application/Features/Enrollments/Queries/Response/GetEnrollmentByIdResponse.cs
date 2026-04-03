using SchoolProject.Domain.Enums;

namespace SchoolProject.Application.Features.Enrollments.Queries.Response
{
    public class GetEnrollmentByIdResponse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public EnrollmentStatus Status { get; set; }
        public float Score { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
