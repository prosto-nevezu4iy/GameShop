using Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.EntityConfigurations
{
    public class ParameterSubGroupConfiguration : EntityTypeConfiguration<ParameterSubGroup>
    {
        public ParameterSubGroupConfiguration()
        {
            Property(pgs => pgs.Name)
                .IsRequired()
                .HasMaxLength(255);

            HasMany(pgs => pgs.Values)
                .WithRequired(pv => pv.SubGroup);
        }
    }
}
