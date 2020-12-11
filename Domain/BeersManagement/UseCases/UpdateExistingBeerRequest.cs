using System;

namespace Domain.BeersManagement.UseCases
{
    public class UpdateExistingBeerRequest
    {
        public Guid Id { get; set; }

        public string Label { get; set; }

        public string Description { get; set; }

        public int Stock { get; set; }
    }
}