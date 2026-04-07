using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Auth.Responses;
using SchoolProject.Domain.Enums;

namespace SchoolProject.Application.Features.Auth.Commands.Models
{
    public class RegisterTeacherCommand : IRequest<Response<RegisterResponse>>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Specialization { get; set; }
        public decimal Salary { get; set; }
    }
}