using Enterprise.Models.Configurations;
using Enterprise.Models.Domains;
using Enterprise.Properties;
using System;
using System.Data.Entity;
using System.Linq;

namespace Enterprise
{
    public class ApplicationDbContext : DbContext
    {
        private static string _conn = $@"Server={Settings.Default.ServerAddress}\{Settings.Default.ServerName};
                                        Database={Settings.Default.DbName};
                                        User Id={Settings.Default.User};
                                        Password={Settings.Default.Password};";
        public ApplicationDbContext()
            : base(_conn)
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