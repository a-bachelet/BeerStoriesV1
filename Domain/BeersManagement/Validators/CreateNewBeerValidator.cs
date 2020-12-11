using Domain.BeersManagement.UseCases;
using FluentValidation;

namespace Domain.BeersManagement.Validators
{
    public class CreateNewBeerValidator : AbstractValidator<CreateNewBeerRequest>
    {
        public CreateNewBeerValidator()
        {
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