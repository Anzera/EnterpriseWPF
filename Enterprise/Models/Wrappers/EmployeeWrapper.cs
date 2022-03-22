using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Models.Wrappers
{
    public class EmployeeWrapper
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salary { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Comments { get; set; }
        public bool Released { get; set; }
        public PositionWrapper Position { get; set; }
    }
}
