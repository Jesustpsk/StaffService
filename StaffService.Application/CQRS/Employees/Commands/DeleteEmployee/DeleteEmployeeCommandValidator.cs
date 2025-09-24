using FluentValidation;

namespace StaffService.Application.CQRS.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty()
            .NotNull();
    }
}