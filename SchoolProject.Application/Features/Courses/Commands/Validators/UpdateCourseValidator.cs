using FluentValidation;
using SchoolProject.Application.Features.Courses.Commands.Models;

namespace SchoolProject.Application.Features.Courses.Commands.Validators
{
    public class UpdateCourseValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Course Id is required");

            RuleFor(x => x.Name)
                .MaximumLength(200).WithMessage("Course name must not exceed 200 characters")
                .When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.TeacherId)
                .GreaterThan(0).WithMessage("Teacher Id must be greater than zero")
                .When(x => x.TeacherId.HasValue);

            RuleFor(x => x.GradeId)
                .GreaterThan(0).WithMessage("Grade Id must be greater than zero")
                .When(x => x.GradeId.HasValue);

            RuleFor(x => x.Credits)
                .GreaterThan(0).WithMessage("Credits must be greater than zero")
                .When(x => x.Credits.HasValue);
        }
    }
}