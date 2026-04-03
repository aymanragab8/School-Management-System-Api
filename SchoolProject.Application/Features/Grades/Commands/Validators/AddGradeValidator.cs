using FluentValidation;
using SchoolProject.Application.Features.Grades.Commands.Models;

namespace SchoolProject.Application.Features.Grades.Commands.Validators
{
    public class AddGradeValidator : AbstractValidator<AddGradeCommand>
    {
        public AddGradeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("The Name must not exceed 100 characters.");

            RuleFor(x => x.Level)
                .NotEmpty().WithMessage("Level is required")
                .GreaterThan(0).WithMessage("Level must be greater than zero.");
        }
    }
}
