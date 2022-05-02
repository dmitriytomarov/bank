using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA
{
    public class Client : INotifyPropertyChanged
    {
        #region InotifyProherty

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

        private string lastName;
        private string firstName;
        private string middleName;
        private string phone;
        private string passport;

        public string ID { get; }

        public string LastName
        {
            get => lastName;
            set { lastName = value; OnPropertyChanged(); }
        }
        public string FirstName
        {
            get => firstName;
            set { firstName = value; OnPropertyChanged(); }
        }
        public string MiddleName
        {
            get => middleName;
            set { middleName = value; OnPropertyChanged(); }
        }
        public string Phone
        {
            get => phone;
            set { phone = value; OnPropertyChanged(); }
        }

        public string Passport
        {
            get => passport;
            set { passport = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Account> _accounts;

        public ObservableCollection<Account> Accounts
        {
            get { return _accounts; }
            set { _accounts = value; }
        }


        public Client(string lastName, string firstName, string middleName, string phone, string passport, string id = "")
        {
            this.lastName =  lastName;
            this.firstName = firstName;
            this.middleName = middleName;
            this.phone = phone;
            this.passport = passport;
            this.ID = (id == "" ? Guid.NewGuid().ToString() : id);
            _accounts = new ObservableCollection<Account>();
        }//конструктор

        public Client() : this("Новый клиент", "", "", "", "") { }  //конструктор по умолчанию
        public Client(string lastName, string firstName, string middleName, string phone, string passport) ://конструктор без ID (в общем то он основной. с ID - по итогу не использую. можно схлопнуть)
            this(lastName, firstName, middleName, phone, passport, "")
        { }
    }
}
