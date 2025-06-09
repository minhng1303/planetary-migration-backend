using FluentValidation;
using PlanetaryMigration.Application.Models;

namespace PlanetaryMigration.Application.Validators
{
    public class CreatePlanetRequestValidator : AbstractValidator<CreatePlanetRequest>
    {
        public CreatePlanetRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Planet name is required.")
                .MaximumLength(4000).WithMessage("Planet name must be at most 4000 characters."); 

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(4000).WithMessage("Description must be at most 4000 characters.");

            RuleFor(x => x.PlanetType)
                .NotEmpty().WithMessage("Planet type is required.");

            RuleFor(p => p.Factors)
       .NotNull().WithMessage("Factors are required.")
       .Must(factors => factors.Count > 0).WithMessage("At least one factor is required.");

            RuleForEach(p => p.Factors)
                .SetValidator(new FactorModelValidator());
        }
    }

    public class FactorModelValidator : AbstractValidator<FactorModel>
    {
        public FactorModelValidator()
        {
            RuleFor(f => f.Value)
                .InclusiveBetween(0, 100).WithMessage("Value must be between 0 and 100.");
        }
    }
}
