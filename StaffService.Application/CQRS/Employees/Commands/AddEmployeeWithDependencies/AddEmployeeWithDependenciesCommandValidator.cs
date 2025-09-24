using System.Data;
using FluentValidation;

namespace StaffService.Application.CQRS.Employees.Commands.AddEmployeeWithDependencies;

public class AddEmployeeWithDependenciesCommandValidator : AbstractValidator<AddEmployeeWithDependenciesCommand>
{
    public AddEmployeeWithDependenciesCommandValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50);
        RuleFor(e => e.Surname)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50);
        RuleFor(e => e.Phone)
            .NotEmpty()
            .NotNull()
            .MaximumLength(20);
        RuleFor(e => e.CompanyId)
            .NotEmpty()
            .NotNull();
        RuleFor(p => p.PassportType)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50);
        RuleFor(p => p.PassportNumber)
            .NotEmpty()
            .NotNull()
            .MaximumLength(20);
        RuleFor(d => d.DepartmentName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(100);
        RuleFor(d => d.DepartmentPhone)
            .NotEmpty()
            .NotNull()
            .MaximumLength(20);
    }
}