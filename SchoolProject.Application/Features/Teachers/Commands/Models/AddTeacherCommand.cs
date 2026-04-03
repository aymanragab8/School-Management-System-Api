using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Domain.Enums;

namespace SchoolProject.Application.Features.Teachers.Commands.Models
{
    public class AddTeacherCommand : IRequest<Response<string>>
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalId { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public string Specialization { get; set; }
        public decimal Salary { get; set; }
    }
}
