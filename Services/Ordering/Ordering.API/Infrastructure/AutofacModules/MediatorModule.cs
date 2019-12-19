using Autofac;
using MediatR;
using Ordering.API.Application.DomainEventHandlers.OrderStartedEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Microsoft.eShopOnContainers.Services.Ordering.API.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Other registrations ...
            // Register the DomainEventHandler classes (they implement 
            // IAsyncNotificationHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(
                typeof(ValidateOrAddBuyerAggregateWhenOrderStartedDomainEventHandler)
                            .GetTypeInfo().Assembly)
                                .AsClosedTypesOf(typeof(INotificationHandler<>));

            // Other registirations ...
            // ...
        }
    }
}
