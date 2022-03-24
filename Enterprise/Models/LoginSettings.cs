using Enterprise.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Models
{
    public class LoginSettings : IDataErrorInfo
    {
        public string LoginUser
        {
            get
            {
                return Settings.Default.LoginUser;
            }
            set
            {
                Settings.Default.LoginUser = value;
            }
        }

        public string LoginPassword 
        {
            get
            {
                return Settings.Default.LoginPassword;
            }

            set 
            {
                Settings.Default.LoginPassword = value;
            } 
        }

        private bool _isLoginValid;
        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(LoginUser):
                        if (string.IsNullOrWhiteSpace(LoginUser))
                        {
                            Error = "Login jest wymagany";
                            _isLoginValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isLoginValid = true;
                        }
                        break;
                    default:
                        break;
                }
                return Error;
            }
        }
        public bool IsValid
        {
            get
            {
                return _isLoginValid;
            }
        }

        public void Save()
        {
            Settings.Default.Save();
        }

    }
}
