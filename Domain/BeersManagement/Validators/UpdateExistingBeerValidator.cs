using Domain.BeersManagement.Services;
using Domain.BeersManagement.UseCases;
using FluentValidation;

namespace Domain.BeersManagement.Validators
{
    public class UpdateExistingBeerValidator : AbstractValidator<UpdateExistingBeerRequest>
    {
        public UpdateExistingBeerValidator(IBeerCatalog catalog)
        {
            RuleFor(beer => beer.Id)
                .Custom((id, context) =>
                {
                    if (catalog.FindOneBeerByGuid(id) == null) context.AddFailure("Unknown beer.");
                })
                .NotNull();

            RuleFor(beer => beer.Label)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(beer => beer.Description)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(beer => beer.Stock)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}