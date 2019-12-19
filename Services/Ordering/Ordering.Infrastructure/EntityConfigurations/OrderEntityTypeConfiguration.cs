using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
using Microsoft.eShopOnContainers.Services.Ordering.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Infrastructure.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders", OrderingContext.DEFAULT_SCHEMA);
            builder.HasKey(o => o.Id);
            builder.Ignore(b => b.DomainEvents);

            // Obsolete
            //builder.Property(o => o.Id)
            //    .ForSqlServerUseSequenceHiLo("orderseq", OrderingContext.DEFAULT_SCHEMA);
            builder.Property(o => o.Id)
                .UseHiLo("orderseq", OrderingContext.DEFAULT_SCHEMA);

            // Address value object persisted as owned entity in EF Core 2.0
            builder.OwnsOne(o => o.Address);

            builder.Property<DateTime>("OrderDate").IsRequired();

            // ... Additional validations, constraints and code...
            // ...
        }
    }
}
