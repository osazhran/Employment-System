using Core.Dtos;
using FluentValidation;
namespace Core.DtosValidator;
//like clint side valdation
public class VacancyRequestValidator: AbstractValidator<VacancyRequest>
{
    public VacancyRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(100)
            .WithMessage("Title must be less than 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(2000)
            .WithMessage("Description must be less than 2000 characters");

        RuleFor(x => x.Requirements)
            .NotEmpty()
            .WithMessage("Requirements is required")
            .MaximumLength(2000)
            .WithMessage("Requirements must be less than 2000 characters");

        RuleFor(x => x.Responsibilities)
            .NotEmpty()
            .WithMessage("Responsibilities is required")
            .MaximumLength(2000)
            .WithMessage("Responsibilities must be less than 2000 characters");

        RuleFor(x => x.MaxApplications)
            .NotEmpty()
            .WithMessage("MaxApplications is required")
            .GreaterThan(0)
            .WithMessage("MaxApplications must be greater than 0");

        RuleFor(x => x.WorkType)
            .NotEmpty()
            .WithMessage("WorkType is required")
            .MaximumLength(50)
            .WithMessage("WorkType must be less than 50 characters");

        RuleFor(x => x.EmploymentType)
            .NotEmpty()
            .WithMessage("EmploymentType is required")
            .MaximumLength(50)
            .WithMessage("EmploymentType must be less than 50 characters");

        RuleFor(x => x.ExperienceYears)
            .NotEmpty()
            .WithMessage("ExperienceYears is required");

        RuleFor(x => x.PositionLevel)
            .NotEmpty()
            .WithMessage("PositionLevel is required")
            .MaximumLength(50)
            .WithMessage("PositionLevel must be less than 50 characters");

        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .WithMessage("CompanyName is required")
            .MaximumLength(100)
            .WithMessage("CompanyName must be less than 100 characters");

        RuleFor(x => x.CompanyAddress)
            .NotEmpty()
            .WithMessage("CompanyAddress is required")
            .MaximumLength(100)
            .WithMessage("CompanyAddress must be less than 100 characters");
    }
}