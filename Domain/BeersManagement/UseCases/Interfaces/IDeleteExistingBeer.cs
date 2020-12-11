namespace Domain.BeersManagement.UseCases.Interfaces
{
    public interface IDeleteExistingBeer
    {
        void Execute(DeleteExistingBeerRequest request, IDeleteExistingBeerPresenter presenter);
    }
}