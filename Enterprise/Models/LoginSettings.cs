using Enterprise.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Models
{
    public class LoginSettings
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
        public void Save()
        {
            Settings.Default.Save();
        }

    }
}
