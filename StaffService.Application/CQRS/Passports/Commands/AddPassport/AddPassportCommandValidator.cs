using FluentValidation;

namespace StaffService.Application.CQRS.Passports.Commands.AddPassport;

public class AddPassportCommandValidator : AbstractValidator<AddPassportCommand>
{
    public AddPassportCommandValidator()
    {
        RuleFor(p => p.Type)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50)
            .WithMessage("Passport type is required.");
        RuleFor(p => p.Number)
            .NotEmpty()
            .NotNull()
            .MaximumLength(20)
            .WithMessage("Passport number is required.");
    }
}