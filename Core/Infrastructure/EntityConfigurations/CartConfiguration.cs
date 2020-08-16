using Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.EntityConfigurations
{
    public class CartConfiguration : EntityTypeConfiguration<Cart>
    {
        public CartConfiguration()
        {
            Property(c => c.CartId)
                .IsRequired();

            HasRequired(c => c.Product)
                .WithMany(p => p.Carts)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
