// EF Core DbContext
public class OrderingContext: DbContext, IUnitofWork
{
    // ...
    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        // Dispach Domain Events collection.
        // Choices:
        // A) Right BEFORE commiting data (EF SaveChanges) into the DB. This makes
        //    a single transaction including side effects from the domain event
        //    handlers that are using the same DbContext with Scope lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB. This makes
        //    multiple transations. You will need to handle eventual consistency and
        //    compensatory actions in case of failures.
        await _mediator.DispachDomainEventsAsync(this);

        // After this line runs, all the changes (from the Command Handler and Domain
        // event handlers) performed through the DbContext will be commited
        var result = await base.SaveChangesAsync();
    }
    // ...
}