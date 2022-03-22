using Enterprise.Models.Converters;
using Enterprise.Models.Domains;
using Enterprise.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise
{
    public class Repository
    {
        public List<EmployeePosition> GetPositions()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Positions.ToList();
            }
        }

        public List<EmployeeWrapper> GetEmployees(int selectedPositionId)
        {
            using (var context = new ApplicationDbContext())
            {
                var employees = context
                    .Employees
                    .Include(x => x.Position)
                    .AsQueryable();

                if (selectedPositionId != 0)
                    employees = employees.Where(x => x.PositionId == selectedPositionId);

                return employees
                    .ToList()
                    .Select(x => x.ToWrapper())
                    .ToList();


            }
        }

        public void DeleteStudent(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var employeeToRelease = context.Employees.Find(id);
                employeeToRelease.ReleaseDate = DateTime.Now;
                employeeToRelease.Released = true;
                context.SaveChanges();
            }
        }
    }
}
