using System.Collections.Generic;
using Domain.BeersManagement.Models;
using FluentValidation.Results;

namespace Domain.BeersManagement.UseCases
{
    public class CreateNewBeerResponse
    {
        public ICollection<ValidationFailure> Errors { get; set; }

        public Beer Beer { get; set; }
    }
}