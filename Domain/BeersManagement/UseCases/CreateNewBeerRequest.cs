namespace Domain.BeersManagement.UseCases
{
    public class CreateNewBeerRequest
    {
        public string Label { get; set; }

        public string Description { get; set; }

        public int Stock { get; set; }
    }
}