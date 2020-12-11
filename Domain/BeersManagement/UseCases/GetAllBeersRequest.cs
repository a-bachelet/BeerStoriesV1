namespace Domain.BeersManagement.UseCases
{
    public class GetAllBeersRequest
    {
        public int? Page { get; set; }

        public int? PerPage { get; set; }
    }
}