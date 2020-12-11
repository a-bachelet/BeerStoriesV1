using System;
using System.Collections.Generic;

namespace Application.ViewModels.BeersManagement
{
    public class ApiGetAllBeersViewModel
    {
        public int HttpCode { get; set; }

        public ICollection<Beer> Data { get; set; }

        public class Beer
        {
            public Guid Id { get; set; }

            public string Label { get; set; }

            public string Description { get; set; }

            public int Stock { get; set; }

            public bool Available { get; set; }

            public bool LastItems { get; set; }
        }
    }
}