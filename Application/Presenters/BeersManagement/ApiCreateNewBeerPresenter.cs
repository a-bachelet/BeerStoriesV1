﻿using System.Collections.Generic;
using System.Linq;
using Application.ViewModels.BeersManagement;
using Domain.BeersManagement.UseCases;
using Domain.BeersManagement.UseCases.Interfaces;

namespace Application.Presenters.BeersManagement
{
    public class ApiCreateNewBeerPresenter : ICreateNewBeerPresenter
    {
        public ApiCreateNewBeerViewModel ViewModel { get; private set; }

        public void Present(CreateNewBeerResponse response)
        {
            var vmHttpCode = response.Beer != null ? 201 : 400;

            var vmErrors = response.Errors?.Count > 0
                ? response.Errors.Select(e =>
                    KeyValuePair.Create(e.PropertyName, e.ErrorMessage)
                ).ToList()
                : null;

            var vmData =
                response.Beer != null
                    ? new ApiCreateNewBeerViewModel.Beer
                    {
                        Id = response.Beer.Id.Value,
                        Label = response.Beer.Label.Value,
                        Description = response.Beer.Description.Value,
                        Stock = response.Beer.Stock.Value,
                        Available = response.Beer.Stock.Value > 0,
                        LastItems = response.Beer.Stock.Value > 0 && response.Beer.Stock.Value <= 10
                    }
                    : null;

            ViewModel = new ApiCreateNewBeerViewModel
            {
                HttpCode = vmHttpCode,
                Errors = vmErrors,
                Data = vmData
            };
        }
    }
}