using Enterprise.Models.Domains;
using Enterprise.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Models.Converters
{
    public static class EmployeeConverter
    {
        public static EmployeeWrapper ToWrapper(this Employee model)
        {
            return new EmployeeWrapper
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                Salary = model.Salary,
                EmploymentDate = model.EmploymentDate,
                ReleaseDate = model.ReleaseDate,
                Released = model.Released,
                Position = new PositionWrapper
                {
                    Id = model.Position.Id,
                    PositionName = model.Position.PositionName
                }
            };
        }
        public static Employee ToDao(this EmployeeWrapper model)
        {
            return new Employee()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                Salary = model.Salary,
                EmploymentDate = model.EmploymentDate,
                ReleaseDate = model.ReleaseDate,
                Released = model.Released,
                PositionId = model.Position.Id
            };
        }
    }
}
