using System;

namespace Domain.BeersManagement.ValueObjects.Beer
{
    public class BeerId
    {
        public BeerId(Guid? value)
        {
            Value = value ?? Guid.NewGuid();
        }

        public Guid Value { get; }
    }
}