using MediatR;
using SchoolProject.Application.Bases;
using System.Text.Json.Serialization;

namespace SchoolProject.Application.Features.Grades.Commands.Models
{
    public class UpdateGradeCommand : IRequest<Response<string>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Level { get; set; }
    }
}
