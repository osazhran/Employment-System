using Core.Dtos;
using FluentValidation;

namespace Core.DtosValidator;
public class LoginRequestValidator: AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is not valid")
            .MaximumLength(100)
            .WithMessage("Email is too long");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters.")
            .Matches("[a-z]")
            .WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[^a-zA-Z0-9]")
            .WithMessage("Password must contain at least one non-alphanumeric character.")
            .Must(x => x.Distinct().Count() >= 3)
            .WithMessage("Password must contain at least 3 unique characters.");
    }
}