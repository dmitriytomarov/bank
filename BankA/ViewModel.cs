﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;

using System.Windows;

namespace BankA
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region InotifyProherty

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

        private NewClientViewModel _newClientVMInstance;

        public string CurrentUser { get; set; } // кто работает в системе Консультант или Менеджер

        public ObservableCollection<Client> ClientsList { get; set; }// список клиентов выбранного департамента

        public Client? CurrentClient { get; set; }  // клиент поля которого редактируются или просматриваются
        public IEmployee User { get; }

        public Client NewClient { get; set; }

        private bool _sortFlag = true;
        public string SortDescription { get; set; } = "Сортировка (а-Я)";
        private Client _selectedClient;// выбранный сейчас клиент
        public Client? SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                CurrentClient = User?.GetInfo(DataBase, SelectedClient);
                LastChanges = User?.GetChanges(DataBase, SelectedClient.ID);
                OnPropertyChanged();
            }
        }


        private Department _selectedDepartment;
        public Department SelectedDepartment    // выделенный в списке департамент
        {
            get => _selectedDepartment;
            set
            {
                _selectedDepartment = value;
                ClientsList = _selectedDepartment.Clients;
                SelectedClient = _selectedDepartment.Clients.FirstOrDefault<Client>();
                OnPropertyChanged();
            }
        }

        private Client _lastChanges;

        public Client? LastChanges { get => _lastChanges; set { _lastChanges = value; OnPropertyChanged(); } }          // данные по последним изменениям (из логов)

        public DataBase DataBase { get; set; }

        public Account SelectedAccount { get; set; }

        public Command Save
        {
            get
            {
                return new Command(o =>
                {
                    User.SetInfo(DataBase, SelectedClient, CurrentClient);
                    CurrentClient = User.GetInfo(DataBase, SelectedClient);
                    LastChanges = User.GetChanges(DataBase, SelectedClient.ID);
                    OnPropertyChanged();
                },
                o => SelectedClient?.LastName != CurrentClient?.LastName ||
                    SelectedClient?.FirstName != CurrentClient?.FirstName ||
                    SelectedClient?.MiddleName != CurrentClient?.MiddleName ||
                    SelectedClient?.Phone != CurrentClient?.Phone ||
                    (SelectedClient?.Passport != CurrentClient?.Passport && CurrentClient?.Passport != "**** ******")
                );
            }
        }

        public Command OpenAccount
        {
            get
            {
                return new Command(o =>
                {
                    OpenAccount w = new();
                    w.Owner = App.Current.MainWindow;
                    w.DataContext = this;
                    w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    w.ShowDialog();
                },
                o => SelectedClient != null
                );
            }
        }

        public bool[] CheckedCurrencyCheckBoxes { get; set; } = { true, false, false, false };
        private readonly Account.Currency[] checkedCurrency = { Account.Currency.RUR, Account.Currency.EUR, Account.Currency.USD, Account.Currency.CNY };
        public Command OpenAccountResultYes
        {
            get
            {
                return new Command(o =>
                {
                    Window t = (Window)o;
                    t.Close();
                    for (int i = 0; i < CheckedCurrencyCheckBoxes.Length; i++)
                    {
                        if (!CheckedCurrencyCheckBoxes[i]) continue;
                        SelectedClient?.Accounts.Add(new StandartCurrentAccount(checkedCurrency[i], SelectedClient));
                    }
                    CheckedCurrencyCheckBoxes[0] = true; // выбор валют счетов по дефолту (только RUR). если убрать то будет запоминаться последний сделанный выбор
                    CheckedCurrencyCheckBoxes[1] = false;
                    CheckedCurrencyCheckBoxes[2] = false;
                    CheckedCurrencyCheckBoxes[3] = false;
                });
            }
        }
        public Command OpenAccountResultNo
        {
            get
            {
                return new Command(o => ((Window)o).Close());
            }
        }
        public Command AddMoneyCommand
        {
            get
            {
                return new Command(o =>
                {
                    var transaction = new Transaction<Account>(SelectedAccount);
                    transaction.AddMoney(SelectedAccount, 100000);

                    //var temp = SelectedClient;
                    //SelectedClient = ClientsList[0];
                    //SelectedClient = temp;

                },
                o => SelectedAccount != null
                );
            }
        }

        public Command PrintLogs
        {
            get
            {
                return new Command((o) =>
                {
                    foreach (var item in DataBase.Logs)
                    {
                        Debug.WriteLine($"{item.ID}  {item.Field} {item.LastChangeTime}  {item.LastChangeUserRole} {item.LastChangeUserName}");
                    }
                });
            }
        }

        public Command AddClientWindow
        {
            get
            {
                return new Command(o =>
                {
                    NewClient windowNewClient = new NewClient();
                    windowNewClient.DataContext = _newClientVMInstance;
                    windowNewClient.Owner = Application.Current.MainWindow;
                    windowNewClient.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    windowNewClient.Title = "New Client";
                    windowNewClient.Show();
                },
                o => (User?.GetType() == typeof(Manager))                                   // canExecute
                );
            }
        }

        public Command Sort
        {
            get => new Command(o =>
            {
                switch (_sortFlag)
                {
                    case true:
                        SelectedDepartment.Clients = new ObservableCollection<Client>(SelectedDepartment.Clients.OrderBy(e => e.LastName));
                        SortDescription = "Сортировка (Я-а)";
                        break;
                    case false:
                        SelectedDepartment.Clients = new ObservableCollection<Client>(SelectedDepartment.Clients.OrderByDescending(e => e.LastName));
                        SortDescription = "Сортировка (а-Я)";
                        break;
                    default: break;
                }
                Client temp = SelectedClient;
                SelectedDepartment = SelectedDepartment;
                SelectedClient = temp;
                _sortFlag = !_sortFlag;
            });
        }


        public ViewModel() { }
        public ViewModel(IEmployee user)
        {
            DataBase = new DataBase();
            SelectedDepartment = DataBase.Departments[0];
            this.User = user;
            CurrentUser = User?.GetType().Name;
            _newClientVMInstance = new NewClientViewModel(this);
            NewClient = new Client("", "", "", "", "", "");


        }
    }

}

