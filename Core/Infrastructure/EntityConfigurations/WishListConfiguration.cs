using Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.EntityConfigurations
{
    public class WishListConfiguration : EntityTypeConfiguration<WishList>
    {
        public WishListConfiguration()
        {
            Property(w => w.WishListId)
                .IsRequired();

            HasRequired(w => w.Product)
                .WithMany(p => p.WishLists)
                .HasForeignKey(w => w.ProductId);
        }
    }
}
