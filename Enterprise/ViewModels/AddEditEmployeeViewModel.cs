using Enterprise.Commands;
using Enterprise.Models.Domains;
using Enterprise.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Enterprise.ViewModels
{
    public class AddEditEmployeeViewModel : BaseViewModel
    {
        private Repository _repository = new Repository();
        public AddEditEmployeeViewModel(EmployeeWrapper employee = null)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);

            if (employee == null)
            {
                Employee = new EmployeeWrapper();
                Employee.EmploymentDate = DateTime.Now;
            }
            else
            {
                Employee = employee;
                IsUpdate = true;
            }


            InitPositions();
        }

        

        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }



        private EmployeeWrapper _employee;

        public EmployeeWrapper Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
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

        private void InitPositions()
        {
            var positions = _repository.GetPositions();
            positions.Insert(0, new EmployeePosition { Id = 0, PositionName = "--brak--" });

            Positions = new ObservableCollection<EmployeePosition>(positions);

            SelectedPositionId = Employee.Position.Id;
        }

        private void Confirm(object obj)
        {
            if (!Employee.IsValid)
                return;

            if (!IsUpdate)
                AddEmployee();
            else
                UpdateEmployee();

            CloseWindow(obj as Window);
        }

        private void UpdateEmployee()
        {
            _repository.UpdateEmployee(Employee);
        }

        private void AddEmployee()
        {
            _repository.AddEmployee(Employee);
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
