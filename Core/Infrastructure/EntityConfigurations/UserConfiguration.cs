using Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.EntityConfigurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(255);

            Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(255);

            Property(u => u.InternalGender)
                .HasColumnName("Gender");

            Ignore(u => u.Gender);
        }
    }
}
