using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Models.Wrappers
{
    public class PositionWrapper : IDataErrorInfo
    {
        public int Id { get; set; }
        public string PositionName { get; set; }

        private bool _isPositionValid;
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Id):
                        if (Id == 0)
                        {
                            Error = "Pole Stanowisko jest wymagane.";
                            _isPositionValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isPositionValid = true;
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

                return _isPositionValid;
            }
        }
    }
}
