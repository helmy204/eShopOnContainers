using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate
{
    public interface IBuyerRepository : IRepository<Buyer>
    {
        Buyer Add(Buyer buyer);
        Buyer Update(Buyer buyer);
        Task<Buyer> FindAsync(string BuyerIdentityGuid);
    }
}
