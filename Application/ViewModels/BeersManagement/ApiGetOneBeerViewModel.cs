﻿using System;

namespace Application.ViewModels.BeersManagement
{
    public class ApiGetOneBeerViewModel
    {
        public int HttpCode { get; set; }

        public Beer Data { get; set; }

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