using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Students.Queries.Response;

namespace SchoolProject.Application.Features.Students.Queries.Models
{
    public class GetStudentByIdQuery : IRequest<Response<GetStudentByIdResponse>>
    {
        public int Id { get; set; }
        public string currentUserId { get; set; }
        public string role { get; set; }
        public GetStudentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
