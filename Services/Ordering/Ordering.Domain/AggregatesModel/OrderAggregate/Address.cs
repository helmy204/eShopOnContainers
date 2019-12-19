using Microsoft.eShopOnContainers.Services.Ordering.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
        
        public Address(string street, string city, string state, string country, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Usin a yield return statment to return each element one at a time
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }
}
