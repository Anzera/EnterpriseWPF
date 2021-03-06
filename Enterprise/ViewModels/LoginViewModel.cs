using Enterprise.Commands;
using Enterprise.Components;
using Enterprise.Models;
using Enterprise.Properties;
using Enterprise.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Enterprise.ViewModels
{
    public class LoginViewModel :BaseViewModel
    { 
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            CloseCommand = new RelayCommand(Close);
            CloseWindowCommand = new RelayCommand(Close);
            _loginSettings = new LoginSettings();
        }

        

        public ICommand LoginCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        private LoginSettings _loginSettings;

        public LoginSettings LoginSettings
        {
            get { return _loginSettings; }
            set 
            { 
                _loginSettings = value;
                OnPropertyChanged();
            }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        private string _user;

        public string User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }
        private void Close(object obj)
        {
            if (string.IsNullOrWhiteSpace(User) || string.IsNullOrWhiteSpace(Password))
                Application.Current.Shutdown();
            else if (Password != LoginSettings.LoginPassword && User != LoginSettings.LoginUser)
                Application.Current.Shutdown();
            else
                CloseWindow(obj as Window);
        }
        private void Login(object obj)
        {
            if (Password == LoginSettings.LoginPassword && User == LoginSettings.LoginUser) 
            { 
                CloseWindow(obj as Window); 
            }
            else
            { 
                MessageBox.Show("Nieporawne dane"); 
            }
        }
        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
