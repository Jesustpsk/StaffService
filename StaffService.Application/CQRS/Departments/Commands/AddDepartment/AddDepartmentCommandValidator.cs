using FluentValidation;

namespace StaffService.Application.CQRS.Departments.Commands.AddDepartment;

public class AddDepartmentCommandValidator : AbstractValidator<AddDepartmentCommand>
{
    public AddDepartmentCommandValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(100)
            .WithMessage("Name is required");
        RuleFor(d => d.Phone)
            .NotEmpty()
            .NotNull()
            .MaximumLength(20)
            .WithMessage("Phone is required");
    }
}