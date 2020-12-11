namespace Domain.BeersManagement.UseCases.Interfaces
{
    public interface IGetAllBeers
    {
        void Execute(GetAllBeersRequest request, IGetAllBersPresenter presenter);
    }
}