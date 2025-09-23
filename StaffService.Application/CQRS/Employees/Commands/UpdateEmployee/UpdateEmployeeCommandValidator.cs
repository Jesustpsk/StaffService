using FluentValidation;

namespace StaffService.Application.CQRS.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("Id cannot be empty");
    }
}