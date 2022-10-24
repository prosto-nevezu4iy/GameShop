using ApplicationCore.Entities.BasketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.OwnsMany(b => b.Items, bi =>
            {
                bi.Property(i => i.UnitPrice)
                    .HasPrecision(10, 2);
            });
        }
    }
}
