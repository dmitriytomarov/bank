using BankA;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace BankA
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region InotifyPropherty

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
                LastChanges = User?.GetChanges(DataBase, SelectedClient?.ID);
                ShowAddAmountTextboxFlag = false;
                //TargetAccountNumber = _selectedClient?.Accounts[0].AccountNumber; //здесь был по дефолту первый счет
                TargetAccountNumber = (_selectedClient?.Accounts.ToList().Find(e => e.AccountCurrency.ToString() == SourceAccountCurrency))?.AccountNumber.ToString() ?? "";
                //здесь ищем счет с валютой соответствующий валюте источника и предлагаем по дефолту его
                VerifyAccountNumber();

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
                ShowAddAmountTextboxFlag = false;
                OnPropertyChanged();
            }
        }

        private Client _lastChanges;


        public Client? LastChanges { get => _lastChanges; set { _lastChanges = value; OnPropertyChanged(); } }          // данные по последним изменениям (из логов)

        public DataBase DataBase { get; set; }

        private Account _targetAccount;

        public Account TargetAccount
        {
            get { return _targetAccount; }
            set { _targetAccount = value; }
        }


        private string _sourceAccountNumber;
        public string SourceAccountNumber
        {
            get { return _sourceAccountNumber; }
            set { _sourceAccountNumber = value; }
        }

        private string _sourceAccountCurrency;  //Source номер и валюта - фикс копии изначально выбраного счета. потом выбирается счет назначения, чтоб не сбрасывались
        public string SourceAccountCurrency
        {
            get { return _sourceAccountCurrency; }
            set { _sourceAccountCurrency = value; }
        }

        
        private string _sourceAccountFormatString;  // в одной строке номер счета и валюта счета
        public string SourceAccountFormatString
        {
            get { return _sourceAccountFormatString; }
            set { _sourceAccountFormatString = value;
                OnPropertyChanged("SourceAccountFormatString");
            }
        }

        private string? _changeBuffer;

        private string _targetAccountNumber;
        public string TargetAccountNumber 
        { 
            get => _targetAccountNumber;
            set
            { 
                _targetAccountNumber = value;
                VerifyAccountNumber();
                OnPropertyChanged();

            }
        }

        

        private Account _selectedAccount;
        public Account SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                ShowAddAmountTextboxFlag = false;
                if (_selectedAccount == null) { UpdateBottomInfoMessage(""); }
                else { UpdateBottomInfoMessage("CTRL + C - скопировать номер счета в буфер");}
                OnPropertyChanged();
            }
        }

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

        // public double AddAmount { get; set; }

        private string _addAmount;

        public string AddAmount
        {
            get { return _addAmount; }
            set
            {
                if (_addAmount != value)
                {
                    _addAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool showAddAmountTextboxFlag = false;

        public bool ShowAddAmountTextboxFlag
        {
            get => showAddAmountTextboxFlag;
            set
            {
                showAddAmountTextboxFlag = value;
                OnPropertyChanged();
            }
        }

        public Command AddMoneyCommandStart
        {
            get
            {
                return new Command(o =>
                {
                    if (SelectedAccount.AccountStatus == Account.Status.Closed) return;
                    AddAmount = "";
                    ShowAddAmountTextboxFlag = true;
                    ((FrameworkElement)o).Focus();
                    //MessageBox.Show(el.IsFocused.ToString());
                    //AddMoneyFocused = true;
                    //App.Current.Windows[0].MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

                },
                o => (SelectedAccount != null && SelectedAccount.AccountStatus != Account.Status.Closed)
                );
            }
        }
        public Command AddMoneyCommandFinish
        {
            get
            {
                return new Command(o =>
                {
                    ShowAddAmountTextboxFlag = false;
                    var transaction = new Transaction<Account>(SelectedAccount);
                    transaction.AddMoney(Convert.ToDecimal(_addAmount));
                },
                o => SelectedAccount != null && Decimal.TryParse(_addAmount, out var res)
                );
            }
        }
        public Command AddMoneyCommandCansel
        {
            get
            {
                return new Command(o =>
                {
                    ShowAddAmountTextboxFlag = false;
                    _addAmount = "";
                });
            }
        }
        public Command CloseAccountCommand
        {
            get
            {
                return new Command(o =>
                {
                    var transaction = new Transaction<Account>(SelectedAccount);
                    transaction.CloseAccount();
                },
                o => SelectedAccount != null
                );
            }
        }

        private string _bottomInfoMessage;
        public string BottomInfoMessage
        {
            get { return _bottomInfoMessage; }
            set { _bottomInfoMessage = value; OnPropertyChanged(); }
        }


        private string _infoMessage;
        public string InfoMessage
        {
            get { return _infoMessage; }
            set { _infoMessage = value; OnPropertyChanged(); }
        }

        private string _totalAmountMessage;

        public string TotalAmountMessage
        {
            get { return _totalAmountMessage; }
            set { _totalAmountMessage = value; OnPropertyChanged(); }
        }


        private string _transferAmount;

        public string TransferAmount
        {
            get { return _transferAmount; }
            set { _transferAmount = value; 
                CreateTotalAmountMessage(); 
            }
        }

        private void CreateTotalAmountMessage()
        {
            TotalAmountMessage = "";
            if (!String.IsNullOrEmpty(InfoMessage))
            {
                TotalAmountMessage = $"Итоговая сумма перевода:  {Convert.ToDecimal(TransferAmount) * 50}";
            }
        }

        private void VerifyAccountNumber()
        {
            InfoMessage = "";
            if (TargetAccountNumber?.Length > 20) { InfoMessage = "Ошибочный номер счета"; return; }
            if (TargetAccountNumber?.Length < 20) { InfoMessage = ""; return; }
            if (TargetAccountNumber==SourceAccountNumber) { InfoMessage = "Номера счетов источника и получателя одинаковы"; return; }

            bool flag = false;
            //Client TargetClient = new();
            foreach (var dep in DataBase.Departments)
            {
                foreach (var client in dep.Clients)
                {
                    foreach (var account in client.Accounts)
                    {
                        if (account.AccountNumber == TargetAccountNumber)
                        {
                            flag = true;
                            TargetAccount = account;
                            _selectedDepartment = dep;
                            //SelectedDepartment = _selectedDepartment;   <- ошибка    //SelectedDepartment = dep; //так вместо пред двух строк - не годится. переполняется стек
                            _selectedClient = client;
                            OnPropertyChanged();
                            break;
                        }
                    }
                    if (flag) break;
                }
                if (flag) break;
            }
            if (!flag) { InfoMessage = "Номер счета не найден."; return; }
            if (TargetAccount?.AccountCurrency.ToString() != SourceAccountCurrency)
            {
                InfoMessage = String.Format("Валюты счетов не совпадают. Будет проведена конвертация из {0} в {1} по курсу {2}.",
                    SourceAccountCurrency, TargetAccount?.AccountCurrency, 50
                    );
            }
            return;
        }

        public Command TransferCommand => new Command(o =>
        {
            if (String.IsNullOrEmpty(SelectedAccount?.AccountNumber)) return; /// ВОПРОС возм убрать5555555
            if (true /*SelectedAccount.AccountCurrency!=TargetAccount.AccountCurrency*/)
            {
            }


        }
        );

        public Command TransferCommandCansel => new Command(tab =>
        {
            ((TabControl)tab).SelectedIndex -= 1;
            SourceAccountNumber = "";
            UpdateBottomInfoMessage("");
        }
     );


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
            DataBase = new DataBase(new TestData());

            SelectedDepartment = DataBase.Departments[0];
            this.User = user;
            CurrentUser = User?.GetType().Name;
            _newClientVMInstance = new NewClientViewModel(this);
            NewClient = new Client("", "", "", "", "", "");


        }

        private Command openTransferTabCommand;
        public ICommand OpenTransferTabCommand => openTransferTabCommand ??= new Command(OpenTransferTab, (o => SelectedAccount != null));

        

        private void OpenTransferTab(object tabs)
        {
            
            ((TabControl)tabs).SelectedIndex += 1;  //переключение на вкладку перевода средств
            SourceAccountNumber = SelectedAccount.AccountNumber;
            SourceAccountCurrency = SelectedAccount.AccountCurrency.ToString();
            SourceAccountFormatString = $"{SourceAccountNumber}   [{SourceAccountCurrency}]";

            UpdateBottomInfoMessage("Выбрать клиента в списке или CTRL + V - вставить номер счета из буфера");
            TargetAccountNumber = "";
            TransferAmount = "10000";

        }

        private void UpdateBottomInfoMessage(string message = "")
        {
            BottomInfoMessage = message;
        }

        private Command copyCurrentAccountNumber;
        //public ICommand CopyCurrentAccountNumber => copyCurrentAccountNumber ??= new Command(accNumb => Clipboard.SetText(accNumb?.ToString()), accNumb => accNumb!=null);
        public ICommand CopyCurrentAccountNumber => copyCurrentAccountNumber ??= new Command(accNumb => _changeBuffer=accNumb?.ToString(), accNumb => accNumb!=null);

        public ICommand InsertAccountNumberCommand => new Command(targetAccComboBox =>
                                                                    {
                                                                        //if (!String.IsNullOrEmpty(Clipboard.GetText()))
                                                                        //{
                                                                        //    TargetAccountNumber = Clipboard.GetText();  // берем номер счета из буфера обмена и ищем по нему Account
                                                                        //    VerifyAccountNumber();
                                                                        //}  // со старндартным буфером имеются странные артефакты и побочки... использую свою переменную 

                                                                        if (!String.IsNullOrEmpty(_changeBuffer))
                                                                        {
                                                                            TargetAccountNumber = _changeBuffer;  // берем номер счета из буфера обмена и ищем по нему Account
                                                                            VerifyAccountNumber();
                                                                        }

                                                                    },
                                                                    o=> !String.IsNullOrEmpty(_changeBuffer)
                                                                    );

    }

}

