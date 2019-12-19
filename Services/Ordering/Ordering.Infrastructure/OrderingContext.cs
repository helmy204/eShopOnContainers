using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using Ordering.Infrastructure.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ordering.Infrastructure;

namespace Microsoft.eShopOnContainers.Services.Ordering.Infrastructure
{
    public class OrderingContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "ordering";

        private readonly IMediator _mediator;

        public OrderingContext(DbContextOptions<OrderingContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            //
            
            // ... Additional type configurations
        }

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
            await _mediator.DispatchDomainEventsAsync(this);

            // After this line runs, all the changes (from the Command Handler and Domain
            // event handlers) performed through the DbContext will be commited
            var result = await base.SaveChangesAsync();

            return true;
        }
    }
}
