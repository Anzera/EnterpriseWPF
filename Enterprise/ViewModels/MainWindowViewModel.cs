using Enterprise.Commands;
using Enterprise.Models.Domains;
using Enterprise.Models.Wrappers;
using Enterprise.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Enterprise.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private Repository _repository = new Repository();
        public MainWindowViewModel()
        {
            //using(var context = new ApplicationDbContext())
            //{
            //    var employees = context.Employees.ToList();
            //}
            AddEmployeeCommand = new RelayCommand(AddEditEmployee);
            EditEmployeeCommand = new RelayCommand(AddEditEmployee, CanEditReleaseEmployee);
            ReleaseEmployeeCommand = new AsyncRelayCommand(ReleaseEmployee, CanEditReleaseEmployee);
            RefrershEmployeeCommand = new RelayCommand(RefrershEmployees);
            InitPositions();
            RefreshEmployeesList();
        }

       
        public ICommand AddEmployeeCommand { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand ReleaseEmployeeCommand { get; set; }
        public ICommand RefrershEmployeeCommand { get; set; }


        private EmployeeWrapper _selectedEmployee;

        public EmployeeWrapper SelectedEmployee
        {
            get { return _selectedEmployee; }
            set 
            { 
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<EmployeeWrapper> _employees;

        public ObservableCollection<EmployeeWrapper> Employees
        {
            get { return _employees; }
            set 
            { 
                _employees = value;
                OnPropertyChanged();
            }
        }

        private int _selectedPositionId;

        public int SelectedPositionId
        {
            get { return _selectedPositionId; }
            set 
            { 
                _selectedPositionId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<EmployeePosition> _positions;

        public ObservableCollection<EmployeePosition> Positions
        {
            get { return _positions; }
            set 
            { 
                _positions = value;
                OnPropertyChanged();
            }
        }

        private void RefrershEmployees(object obj)
        {
            RefreshEmployeesList();    
        }

        private void RefreshEmployeesList()
        {
            Employees = new ObservableCollection<EmployeeWrapper>(
                _repository.GetEmployees(SelectedPositionId));
        }

        private async Task ReleaseEmployee(object arg)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync(
                "Zwalnianie pracownika",
                $"Czy na pewno zwolnić pracownika {SelectedEmployee.FirstName} {SelectedEmployee.LastName}?",
                MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            _repository.DeleteEmployee(SelectedEmployee.Id);

            RefreshEmployeesList();
        }

        private bool CanEditReleaseEmployee(object obj)
        {
            return SelectedEmployee != null && SelectedEmployee.Released != true;
        }

        private void AddEditEmployee(object obj)
        {
            var addeditEmployeeWindow = new AddEditEmplyee();
            addeditEmployeeWindow.Closed += addeditEmployeeWindow_Closed;
            addeditEmployeeWindow.ShowDialog();
        }

        private void addeditEmployeeWindow_Closed(object sender, EventArgs e)
        {
            RefreshEmployeesList();
        }

        private void InitPositions()
        {
            var positions = _repository.GetPositions();
            positions.Insert(0, new EmployeePosition { Id = 0, PositionName = "Wszystkie stanowiska" });

            Positions = new ObservableCollection<EmployeePosition>(positions);

            SelectedPositionId = 0;
        }


    }
}
