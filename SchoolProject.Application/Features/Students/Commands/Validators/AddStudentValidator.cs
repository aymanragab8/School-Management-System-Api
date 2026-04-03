using FluentValidation;
using SchoolProject.Application.Features.Students.Commands.Models;

namespace SchoolProject.Application.Features.Students.Commands.Validators
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        public AddStudentValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("الاسم مطلوب")
                .MaximumLength(100).WithMessage("الاسم لا يتجاوز 100 حرف");

            RuleFor(x => x.NationalId)
                .NotEmpty().WithMessage("الرقم القومي مطلوب")
                .Length(14).WithMessage("الرقم القومي لازم يكون 14 رقم")
                .Matches("^[0-9]*$").WithMessage("الرقم القومي أرقام بس");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("رقم التليفون مطلوب")
                .Matches("^01[0125][0-9]{8}$").WithMessage("رقم التليفون غير صحيح");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("تاريخ الميلاد مطلوب")
                .LessThan(DateTime.Now).WithMessage("تاريخ الميلاد غير صحيح");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("العنوان مطلوب");
        }
    }
}
