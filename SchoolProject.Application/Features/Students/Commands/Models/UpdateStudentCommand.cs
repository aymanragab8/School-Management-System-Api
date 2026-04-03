using MediatR;
using SchoolProject.Application.Bases;
using System.Text.Json.Serialization;

namespace SchoolProject.Application.Features.Students.Commands.Models
{
    public class UpdateStudentCommand : IRequest<Response<string>>
    {
        [JsonIgnore]
        public int StudentId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int? GradeId { get; set; }
        [JsonIgnore]
        public string? currentUserId { get; set; }
        [JsonIgnore]
        public string? role { get; set; }
    }
}