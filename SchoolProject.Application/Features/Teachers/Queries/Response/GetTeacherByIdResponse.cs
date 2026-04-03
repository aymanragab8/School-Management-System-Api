using SchoolProject.Domain.Enums;
using System.Text.Json.Serialization;

namespace SchoolProject.Application.Features.Teachers.Queries.Response
{
    public class GetTeacherByIdResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Specialization { get; set; }
        public decimal Salary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<Courses> Courses { get; set; }
    }
    public class Courses
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
}
