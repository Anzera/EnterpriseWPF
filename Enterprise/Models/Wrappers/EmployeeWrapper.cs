using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Models.Wrappers
{
    public class EmployeeWrapper : IDataErrorInfo
    {
        public EmployeeWrapper()
        {
            Position = new PositionWrapper();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Comments { get; set; }
        public bool Released { get; set; }
        public PositionWrapper Position { get; set; }

        private bool _isFirstNameValid;
        private bool _isLastNameValid;
        private bool _isEmploymentDateValid;
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrWhiteSpace(FirstName))
                        {
                            Error = "Pole Imię jest wymagane.";
                            _isFirstNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isFirstNameValid = true;
                        }
                        break;
                    case nameof(LastName):
                        if (string.IsNullOrWhiteSpace(LastName))
                        {
                            Error = "Pole Nazwisko jest wymagane.";
                            _isLastNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isLastNameValid = true;
                        }
                        break;
                    case nameof(EmploymentDate):
                        if (string.IsNullOrWhiteSpace(EmploymentDate.ToString()))
                        {
                            Error = "Pole Data zatrudnienia jest wymagane.";
                            _isEmploymentDateValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isEmploymentDateValid = true;
                        }
                        break;
                    default:
                        break;
                }

                return Error;
            }
        }
        public string Error { get; set; }

        public bool IsValid
        {
            get
            {
               
                return _isFirstNameValid && _isLastNameValid && _isEmploymentDateValid && Position.IsValid;
            }
        }
    }
}
