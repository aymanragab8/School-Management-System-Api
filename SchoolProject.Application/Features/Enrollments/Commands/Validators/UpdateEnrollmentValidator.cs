using FluentValidation;
using SchoolProject.Application.Features.Enrollments.Commands.Models;

namespace SchoolProject.Application.Features.Enrollments.Commands.Validators
{
    partial class UpdateEnrollmentValidator : AbstractValidator<UpdateEnrollmentCommand>
    {
        public UpdateEnrollmentValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Enrollment Id is required");

            RuleFor(x => x.Score)
                .InclusiveBetween(0, 100).WithMessage("Score must be between 0 and 100")
                .When(x => x.Score.HasValue);
        }
    }
}
