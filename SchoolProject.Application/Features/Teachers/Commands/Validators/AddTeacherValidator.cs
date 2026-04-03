using FluentValidation;
using SchoolProject.Application.Features.Teachers.Commands.Models;

namespace SchoolProject.Application.Features.Teachers.Commands.Validators
{
    public class AddTeacherValidator : AbstractValidator<AddTeacherCommand>
    {
        public AddTeacherValidator()
        {

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name is required")
                .MaximumLength(100).WithMessage("The Full Name must not exceed 100 characters.");

            RuleFor(x => x.NationalId)
                .NotEmpty().WithMessage("National Id is required")
                .Length(14).WithMessage("The national ID number must be 14 digits.")
                .Matches("^[0-9]*$").WithMessage("National ID must be just numbers.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required.")
                .Matches("^01[0125][0-9]{8}$").WithMessage("Incorrect phone number.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date Of Birth is required.")
                .LessThan(DateTime.Now).WithMessage("Incorrect date of birth.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");

            RuleFor(x => x.Specialization)
                .NotEmpty().WithMessage("Specialization is required")
                .MaximumLength(200).WithMessage("Specialization must not exceed 200 characters.");

            RuleFor(x => x.Salary)
                .NotNull().WithMessage("Salary is required")
                .GreaterThan(0).WithMessage("The salary must be greater than zero.");

        }
    }
}
