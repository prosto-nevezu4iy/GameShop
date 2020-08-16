using Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.EntityConfigurations
{
    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            Property(o => o.FirstName)
                .IsRequired()
                .HasMaxLength(160);

            Property(o => o.LastName)
                .IsRequired()
                .HasMaxLength(160);

            Property(o => o.Phone)
                .IsRequired()
                .HasMaxLength(24);

            Property(o => o.Email)
                .IsRequired()
                .HasMaxLength(255);

            Property(o => o.Country)
                .IsRequired()
                .HasMaxLength(40);

            Property(o => o.City)
                .IsRequired()
                .HasMaxLength(40);

            Property(o => o.Address)
                .IsRequired()
                .HasMaxLength(70);

            Property(o => o.PostalCode)
                .IsRequired()
                .HasMaxLength(10);

            HasMany(o => o.OrderDetails)
                .WithRequired(od => od.Order);
        }
    }
}
