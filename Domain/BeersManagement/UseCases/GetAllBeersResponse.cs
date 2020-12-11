using System.Collections.Generic;
using Domain.BeersManagement.Models;

namespace Domain.BeersManagement.UseCases
{
    public class GetAllBeersResponse
    {
        public ICollection<Beer> Beers { get; set; }
    }
}