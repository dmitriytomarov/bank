using System;
using System.Collections.Generic;
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

        public Client(string lastName, string firstName, string middleName, string phone, string passport, string id = "")
        {
            this.lastName =  lastName;
            this.firstName = firstName;
            this.middleName = middleName;
            this.phone = phone;
            this.passport = passport;
            this.ID = (id == "" ? Guid.NewGuid().ToString() : id);
        }//конструктор
        public Client() : this("Новый клиент", "", "", "", "") { }  //конструктор по умолчанию
        public Client(string lastName, string firstName, string middleName, string phone, string passport) ://конструктор без ID (нужен)
            this(lastName, firstName, middleName, phone, passport, "")
        { }
    }
}
