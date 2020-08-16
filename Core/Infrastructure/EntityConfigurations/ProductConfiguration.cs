using Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.EntityConfigurations
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(p => p.Alias)
                .IsRequired()
                .HasMaxLength(255);

            Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(2000);

            HasRequired(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            HasMany(p => p.Values)
                .WithMany(v => v.Products)
                .Map(pv =>
                {
                    pv.MapLeftKey("ProductId");
                    pv.MapRightKey("ValueId");
                    pv.ToTable("ProductValues");
                });
        }
    }
}
