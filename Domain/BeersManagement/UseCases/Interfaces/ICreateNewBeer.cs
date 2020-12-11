namespace Domain.BeersManagement.UseCases.Interfaces
{
    public interface ICreateNewBeer
    {
        void Execute(CreateNewBeerRequest request, ICreateNewBeerPresenter presenter);
    }
}