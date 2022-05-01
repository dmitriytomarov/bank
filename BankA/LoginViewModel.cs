using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BankA
{
    public class LoginViewModel : ViewModel
    {

        private MainWindow? _mainWindow;
        private ViewModel? _vMInstance;

        private string? _login;
        private string? _password;
        public List<IEmployee> Employees { get; }



        public string? Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }
        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }


        public static string? SelectedUser { get; private set; }

        public Command Auth
        {
            get => new Command(
                                o => Autorize(Login, Password),
                                o => !String.IsNullOrWhiteSpace(Login) && !String.IsNullOrWhiteSpace(Password));
        }

        private void Autorize(string? login, string? password)
        {
            IEmployee? user = Employees.Find(e => e.Login == login);

            if (user == null)
            {
                MessageBox.Show("Пользователь не найден");
                Login = "";
                Password = "";
                App.Current.Windows[0].MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            }
            else if (user.CheckPassword(password))
            {
                StartMainWindow(user);
            }
            else
            {
                MessageBox.Show("Неверный пароль");
                Password = "";
                App.Current.Windows[0].MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
            }
        }

        

        private void StartMainWindow(IEmployee authorizedUser)
        {
            _mainWindow = new MainWindow();
            _vMInstance = new ViewModel(authorizedUser);
            _mainWindow.DataContext = _vMInstance;
            App.Current.Windows[0].Close();
            _mainWindow.Show();
            _vMInstance.SelectedDepartment = _vMInstance.DataBase.Departments[0];
            _vMInstance.SelectedClient = _vMInstance.SelectedDepartment.Clients[0];
            //Application.Current.MainWindow  - вот так написать можно, а вот так нельзя, странно  - Application.Current.Login

        }
        public LoginViewModel()
        {
            Employees = Consultant.CreateTestUsers();
            Login = "malkov";
            Password = "789";
        }
    }
}
