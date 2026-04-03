using FluentValidation;
using SchoolProject.Application.Features.Enrollments.Commands.Models;

namespace SchoolProject.Application.Features.Enrollments.Commands.Validators
{
    public class AddEnrollmentValidator : AbstractValidator<AddEnrollmentCommand>
    {
        public AddEnrollmentValidator()
        {
            RuleFor(x => x.CourseId)
                .NotNull().WithMessage("Course Id is required")
                .GreaterThan(0).WithMessage("The course Id must be greater than zero.");

            RuleFor(x => x.StudentId)
                .NotNull().WithMessage("Student Id is required")
                .GreaterThan(0).WithMessage("The student Id must be greater than zero.");

        }
    }
}
