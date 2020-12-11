namespace Domain.BeersManagement.UseCases.Interfaces
{
    public interface IUpdateExistingBeer
    {
        void Execute(UpdateExistingBeerRequest request, IUpdateExistingBeerPresenter presenter);
    }
}