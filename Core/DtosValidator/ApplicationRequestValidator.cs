using Core.Dtos;
using FluentValidation;

namespace Core.DtosValidator;
public class ApplicationRequestValidator: AbstractValidator<ApplicationRequest>
{
    public ApplicationRequestValidator()
    {
        RuleFor(x => x.VacancyId)
            .GreaterThan(0)
            .WithMessage("VacancyId must be greater than 0");

        RuleFor(x => x.ResumeUrl)
            .NotEmpty()
            .WithMessage("ResumeUrl must not be empty")
            .MaximumLength(255)
            .WithMessage("ResumeUrl must not be longer than 255 characters");
    }
}
