﻿using Enterprise.Commands;
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
        private bool _close;

        public UserSettingsViewModel(bool close)
        {
            CloseSettingsCommand = new RelayCommand(Close);
            ConfirmSettingsCommand = new RelayCommand(Confirm);
            _userSettings = new UserSettings();
            _close = close;
        }

        public ICommand CloseSettingsCommand { get; set; }
        public ICommand ConfirmSettingsCommand { get; set; }

        private void Confirm(object obj)
        {
            if (!UserSettings.IsValid)
                return;

            CloseWindow(obj as Window);
            RestartAsync();
        }

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

        private void Close(object obj)
        {
            if (_close)
                Application.Current.Shutdown();
            CloseWindow(obj as Window);

        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
