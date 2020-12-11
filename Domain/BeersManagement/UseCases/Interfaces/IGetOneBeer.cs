namespace Domain.BeersManagement.UseCases.Interfaces
{
    public interface IGetOneBeer
    {
        void Execute(GetOneBeerRequest request, IGetOneBeerPresenter presenter);
    }
}