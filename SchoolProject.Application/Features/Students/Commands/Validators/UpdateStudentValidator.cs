using FluentValidation;
using SchoolProject.Application.Features.Students.Commands.Models;

namespace SchoolProject.Application.Features.Students.Commands.Validators
{
    public class UpdateStudentValidator : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("يجب املاء الحقول المطلوبه");

            RuleFor(x => x.FullName)
                .MaximumLength(100).WithMessage("الاسم لا يتجاوز 100 حرف")
                .When(x => !string.IsNullOrEmpty(x.FullName))
                .WithMessage("الاسم مطلوب");

            RuleFor(x => x.PhoneNumber)
                .Matches("^01[0125][0-9]{8}$").WithMessage("رقم التليفون غير صحيح")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage("رقم التليفون مطلوب");

            RuleFor(x => x.Address)
                .MaximumLength(200).WithMessage("العنوان لا يتجاوز 200 حرف")
                .When(x => !string.IsNullOrEmpty(x.Address))
                .WithMessage("العنوان مطلوب");

            RuleFor(x => x.GradeId)
                .GreaterThan(0).WithMessage("الصف مطلوب")
                .When(x => x.GradeId.HasValue)
                .WithMessage("يجب ان تدخل رقم الصف صحيح");
        }
    }
}
