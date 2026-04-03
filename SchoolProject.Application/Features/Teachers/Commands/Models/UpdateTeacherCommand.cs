using MediatR;
using SchoolProject.Application.Bases;
using System.Text.Json.Serialization;

namespace SchoolProject.Application.Features.Teachers.Commands.Models
{
    public class UpdateTeacherCommand : IRequest<Response<string>>
    {
        [JsonIgnore]
        public int TeacherId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public decimal? Salary { get; set; }
        [JsonIgnore]
        public string? currentUserId { get; set; }
        [JsonIgnore]
        public string? role { get; set; }

    }
}
