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
                            .AsClosedTypesOf(typeof(IAsyncNotificationHandler<>));

		// Other registirations ...
		// ...
    }
}