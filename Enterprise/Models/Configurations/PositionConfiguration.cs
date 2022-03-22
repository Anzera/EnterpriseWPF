using Enterprise.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Models.Configurations
{
    public class PositionConfiguration : EntityTypeConfiguration<EmployeePosition>
    {
        public PositionConfiguration()
        {
            ToTable("dbo.Positios");

            HasKey(x => x.Id);
        }
    }
}
