using SchoolProject.Domain.Enums;
using System.Text.Json.Serialization;

namespace SchoolProject.Application.Features.Students.Queries.Response
{
    public class GetStudentByIdResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string GradeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<EnrollmentResponse> Enrollments { get; set; } = new();
    }
    public class EnrollmentResponse
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public float? Score { get; set; }
        public EnrollmentStatus Status { get; set; }
    }
}
