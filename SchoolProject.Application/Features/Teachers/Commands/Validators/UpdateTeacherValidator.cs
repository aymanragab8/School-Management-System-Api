using FluentValidation;
using SchoolProject.Application.Features.Teachers.Commands.Models;

namespace SchoolProject.Application.Features.Teachers.Commands.Validators
{
    public class UpdateTeacherValidator : AbstractValidator<UpdateTeacherCommand>
    {
        public UpdateTeacherValidator()
        {
            RuleFor(x => x)
               .NotNull()
               .WithMessage("The required fields must be filled in.");

            RuleFor(x => x.FullName)
                .MaximumLength(100).WithMessage("The FullName must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FullName))
                .WithMessage("Name is required");

            RuleFor(x => x.PhoneNumber)
                .Matches("^01[0125][0-9]{8}$").WithMessage("Incorrect phone number")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage("Phone Number is required");

            RuleFor(x => x.Address)
                .MaximumLength(200).WithMessage("The Address should not exceed 200 characters.")
                .When(x => !string.IsNullOrEmpty(x.Address))
                .WithMessage("Address is required");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("The salary must be greater than zero.");
        }
    }
}
