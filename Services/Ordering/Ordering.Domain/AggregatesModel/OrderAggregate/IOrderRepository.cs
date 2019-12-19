using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order Add(Order order);
        void Update(Order order);
        Task<Order> GetAsync(int orderId);
    }
}
