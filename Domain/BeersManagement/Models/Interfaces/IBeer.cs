namespace Domain.BeersManagement.Models.Interfaces
{
    public interface IBeer
    {
        public void IncreaseStock(int stock);

        public void DecreaseStock(int stock);
    }
}