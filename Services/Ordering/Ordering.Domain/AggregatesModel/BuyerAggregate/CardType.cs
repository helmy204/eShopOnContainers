using Microsoft.eShopOnContainers.Services.Ordering.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate
{
    public class CardType : Enumeration
    {
        public static CardType Amex = new CardType(1, "Amex");
        public static CardType Visa = new CardType(2, "Visa");
        public static CardType MasterCard = new CardType(3, "MasterCard");

        protected CardType() { }
        public CardType(int id, string name)
           : base(id, name)
        {
        }

        public static IEnumerable<CardType> List() => Enumeration.GetAll<CardType>();

        // Other util methods
    }
}
