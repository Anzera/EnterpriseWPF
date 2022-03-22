using Enterprise.Models.Configurations;
using Enterprise.Models.Domains;
using System;
using System.Data.Entity;
using System.Linq;

namespace Enterprise
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeePosition> Positions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new PositionConfiguration());
        }

    }
}