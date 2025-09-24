using System.Data;
using FluentValidation;

namespace StaffService.Application.CQRS.Employees.Commands.UpdateEmployeeWithDependencies;

public class UpdateEmployeeWithDependenciesCommandValidator : AbstractValidator<UpdateEmployeeWithDependenciesCommand>
{
    public UpdateEmployeeWithDependenciesCommandValidator()
    {
        RuleFor(e => e.Id)
            .NotNull()
            .NotEmpty();
    }
}