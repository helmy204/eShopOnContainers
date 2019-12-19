using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.SeedWork;

namespace Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public class OrderStatus : Enumeration
    {
        public static OrderStatus Submitted = new OrderStatus(1, nameof(Submitted).ToLowerInvariant());

        public OrderStatus(int id, string name)
            : base(id, name)
        {
        }
    }
}
