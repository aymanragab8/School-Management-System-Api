using FluentValidation;
using SchoolProject.Application.Features.Courses.Commands.Models;

namespace SchoolProject.Application.Features.Courses.Commands.Validators
{
    public class AddCourseValidator : AbstractValidator<AddCourseCommand>
    {
        public AddCourseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Course name is required")
                .MaximumLength(200).WithMessage("Course name must not exceed 200 characters");

            RuleFor(x => x.TeacherId)
                .GreaterThan(0).WithMessage("Teacher is required");

            RuleFor(x => x.GradeId)
                .GreaterThan(0).WithMessage("Grade is required");

            RuleFor(x => x.Credits)
                .GreaterThan(0).WithMessage("Credits must be greater than zero");
        }
    }
}