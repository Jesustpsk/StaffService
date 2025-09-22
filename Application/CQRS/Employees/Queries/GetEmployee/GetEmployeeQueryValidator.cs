using FluentValidation;

namespace Application.CQRS.Employees.Queries.GetEmployee;

public class GetEmployeeQueryValidator : AbstractValidator<GetEmployeeQuery>
{
    public GetEmployeeQueryValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("Id is required.");
    }
}