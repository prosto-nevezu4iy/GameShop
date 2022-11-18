using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShipToAddress, a =>
            {
                a.WithOwner();

                a.Property(a => a.ZipCode)
                    .HasMaxLength(18)
                    .IsRequired();

                a.Property(a => a.Street)
                    .HasMaxLength(180)
                    .IsRequired();

                a.Property(a => a.State)
                    .HasMaxLength(60);

                a.Property(a => a.Country)
                    .HasMaxLength(90)
                    .IsRequired();

                a.Property(a => a.City)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            builder.Navigation(x => x.ShipToAddress).IsRequired();

            builder.OwnsMany(o => o.OrderItems, oi =>
            {
                oi.Property(x => x.UnitPrice)
                    .HasPrecision(18, 2);

                oi.OwnsOne(x => x.ProductOrdered, po =>
                {
                    po.Property(x => x.ProductName)
                        .HasMaxLength(50)
                        .IsRequired();
                });
            });
        }
    }
}
