using MediatR;
using SchoolProject.Application.Bases;
using System.Text.Json.Serialization;

namespace SchoolProject.Application.Features.Enrollments.Commands.Models
{
    public class UpdateEnrollmentCommand : IRequest<Response<string>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public float? Score { get; set; }

    }
}
