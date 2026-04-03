using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Teachers.Queries.Response;

namespace SchoolProject.Application.Features.Teachers.Queries.Models
{
    public class GetTeacherByIdQuery : IRequest<Response<GetTeacherByIdResponse>>
    {
        public int Id { get; set; }
        public string currentUserId { get; set; }
        public string role { get; set; }

        public GetTeacherByIdQuery(int id)
        {
            Id = id;
        }
    }

}
