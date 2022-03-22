using Enterprise.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Models.Configurations
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            ToTable("dbo.Employees");

            HasKey(x => x.Id);

            Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Released)
                .IsOptional();
        
        }
    }
}
