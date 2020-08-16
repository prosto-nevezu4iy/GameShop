using Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.EntityConfigurations
{
    public class ParameterGroupConfiguration : EntityTypeConfiguration<ParameterGroup>
    {
        public ParameterGroupConfiguration()
        {
            Property(pg => pg.Name)
                .IsRequired()
                .HasMaxLength(255);

            HasMany(pg => pg.SubGroups)
                .WithRequired(pgs => pgs.Group);
        }
    }
}
