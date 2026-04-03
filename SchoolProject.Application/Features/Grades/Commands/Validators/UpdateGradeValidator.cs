using FluentValidation;
using SchoolProject.Application.Features.Grades.Commands.Models;

namespace SchoolProject.Application.Features.Grades.Commands.Validators
{
    public class UpdateGradeValidator : AbstractValidator<UpdateGradeCommand>
    {
        public UpdateGradeValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("The required fields must be filled in.");

            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage("Name is required");

            RuleFor(x => x.Level)
                .GreaterThan(0).WithMessage("Level must be greater than zero.");

        }

    }
}
