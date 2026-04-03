using MediatR;
using SchoolProject.Application.Bases;
using System.Text.Json.Serialization;

namespace SchoolProject.Application.Features.Courses.Commands.Models
{
    public class UpdateCourseCommand : IRequest<Response<string>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public string role { get; set; }
        [JsonIgnore]
        public string currentUserId { get; set; }
        public int? TeacherId { get; set; }
        public int? GradeId { get; set; }
        public string? Name { get; set; }
        public int? Credits { get; set; }
    }
}
