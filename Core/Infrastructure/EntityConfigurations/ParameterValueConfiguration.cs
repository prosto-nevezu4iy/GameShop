using Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.EntityConfigurations
{
    public class ParameterValueConfiguration : EntityTypeConfiguration<ParameterValue>
    {
        public ParameterValueConfiguration()
        {
            Property(pv => pv.Value)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
