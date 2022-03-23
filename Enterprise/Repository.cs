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

        public void ReleaseEmployee(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var employeeToRelease = context.Employees.Find(id);
                employeeToRelease.ReleaseDate = DateTime.Now;
                employeeToRelease.Released = true;
                context.SaveChanges();
            }
        }

        public void AddEmployee(EmployeeWrapper employeeWrapper)
        {
            var employee = employeeWrapper.ToDao();

            using(var context = new ApplicationDbContext())
            {
                var emploeeToAdd = context.Employees.Add(employee);
                context.SaveChanges();
            }
        }

        public void UpdateEmployee(EmployeeWrapper employeeWrapper)
        {
            var employee = employeeWrapper.ToDao();

            using (var context = new ApplicationDbContext())
            {
                var emploeeToUpdate = context.Employees.Find(employee.Id);
                emploeeToUpdate.FirstName = employee.FirstName;
                emploeeToUpdate.LastName = employee.LastName;
                emploeeToUpdate.Comments = employee.Comments;
                emploeeToUpdate.Salary = employee.Salary;
                emploeeToUpdate.Position = employee.Position;
                emploeeToUpdate.EmploymentDate = employee.EmploymentDate;
                context.SaveChanges();
            }
        }
    }
}
