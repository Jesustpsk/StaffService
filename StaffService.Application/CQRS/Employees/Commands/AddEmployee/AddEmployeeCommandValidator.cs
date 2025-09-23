using FluentValidation;

namespace StaffService.Application.CQRS.Employees.Commands.AddEmployee;

public class AddEmployeeCommandValidator : AbstractValidator<AddEmployeeCommand>
{
    public AddEmployeeCommandValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(50)
            .WithMessage("Name must be between 3 and 50 characters");
        RuleFor(e => e.Surname)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(50)
            .WithMessage("Surname must be between 3 and 50 characters");
        RuleFor(e => e.Phone)
            .NotEmpty()
            .NotNull()
            .MinimumLength(10)
            .MaximumLength(20)
            .WithMessage("Phone must be between 10 and 20 characters");
        RuleFor(e => e.CompanyId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Company Id cannot be empty");
        RuleFor(e => e.PassportId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Passport Id cannot be empty");
        RuleFor(e => e.DepartmentId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Department Id cannot be empty");
    }
}