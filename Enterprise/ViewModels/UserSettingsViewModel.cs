using Enterprise.Commands;
using Enterprise.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Enterprise.ViewModels
{
    public class UserSettingsViewModel : BaseViewModel
    {
        private UserSettings _userSettings;
        private LoginSettings _loginSettings;
        private bool _close;

        public UserSettingsViewModel(bool close)
        {
            CloseSettingsCommand = new RelayCommand(Close);
            ConfirmSettingsCommand = new RelayCommand(ConfirmBaseSettings);
            ConfirmLoginCommand = new RelayCommand(CloseLoginSettings);
            _userSettings = new UserSettings();
            _loginSettings = new LoginSettings();
            _close = close;
            if (!close)
            {
                IsUpdate = true;
            }
        }


        public ICommand CloseSettingsCommand { get; set; }
        public ICommand ConfirmSettingsCommand { get; set; }
        public ICommand ConfirmLoginCommand { get; set; }

        

        public UserSettings UserSettings
        {
            get
            {
                return _userSettings;
            }
            set
            {
                _userSettings = value;
                OnPropertyChanged();
            }
        }

        public LoginSettings LoginSettings
        {
            get
            {
                return _loginSettings;
            }
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

        private string _passwordBis;

        public string PasswordBis
        {
            get { return _passwordBis; }
            set
            {
                _passwordBis = value;
                OnPropertyChanged();
            }
        }
        

        private bool _isUpdade;
        public bool IsUpdate
        {
            get { return _isUpdade; }
            set
            {
                _isUpdade = value;
                OnPropertyChanged();
            }
        }

        private async Task RestartAsync()
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync(
                "Ustawienia połączenia z bazą danych",
                "Czy na pewno chcesz zmienić ustawienia połączenia z bazą danych?",
                MessageDialogStyle.AffirmativeAndNegative);


            if (dialog == MessageDialogResult.Affirmative)
            {
                UserSettings.Save();
                Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
        }

        private void ConfirmBaseSettings(object obj)
        {
            if (!UserSettings.IsValid)
                return;

            CloseWindow(obj as Window);
            RestartAsync();
        }

        private void Close(object obj)
        {
            if (_close)
                Application.Current.Shutdown();
            CloseWindow(obj as Window);

        }


        private void CloseLoginSettings(object obj)
        {
            if (!LoginSettings.IsValid)
                return;
            if (Password != PasswordBis)
            {
                MessageBox.Show("Hasła nie są identyczne");
                return;
            }

            PasswordChange();
            CloseWindow(obj as Window);

        }

        private void PasswordChange()
        {
            LoginSettings.LoginPassword = Password;
            LoginSettings.Save();
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
