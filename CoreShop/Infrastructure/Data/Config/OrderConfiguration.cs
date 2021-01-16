using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, s => s.WithOwner());
            builder.Property(o => o.Status).HasConversion(
                s => s.ToString(), // destination
                s => (OrderStatus)Enum.Parse(typeof(OrderStatus), s)); // source
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade); // composition
        }
    }
}
