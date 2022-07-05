using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA
{
    public abstract class Account : INotifyPropertyChanged
    {
        #region InotifyProherty

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

        public string AccountNumber { get; private set; }
        private decimal money;
        public decimal Money
        {
            get => money;
            set
            {
                money = value;
                OnPropertyChanged();
            }
        }
        public Currency AccountCurrency { get; init; }
        public DateTime OpenDate { get; private set; }

        private Status _accountStatus;
        public Status AccountStatus
        {
            get => _accountStatus;
            set
            {
            _accountStatus = value; OnPropertyChanged();
            }
        }




        public enum Status { Reserved, Actual, Blocked, Closed, Undefined }
        public enum Currency { EUR = 978, RUR = 810, USD = 840, CNY = 156 }

        public static Dictionary<string, Client> AllBankAccounts { get; set; }
        static Account()
        {
            AllBankAccounts = new();
            index = 0;
        }


        private static int index = 0;

        protected virtual string GetNewAccountNummber(Currency cur)
        {
            StringBuilder s = new(20);
            s.Append("407")
            .Append("17")
            .Append((int)cur)
            .Append("00000")
            .Append((++index).ToString("0000000"));

            return s.ToString();
        }

        public Account(Currency cur, Client targetClient)
        {
            AccountNumber = GetNewAccountNummber(cur);
            AllBankAccounts.Add(this.AccountNumber, targetClient);
            AccountCurrency = cur;
            Money = 0;
            OpenDate = DateTime.Now;
            AccountStatus = Status.Actual;
        }

    }

    public class StandartCurrentAccount : Account
    {
        public StandartCurrentAccount(Currency cur, Client targetClient) : base(cur, targetClient) { }
    }


    public class DepositAccount : Account
    {
        public decimal DepositRate { get; } 
        public DepositAccount(Currency cur, Client targetClient) : base(cur, targetClient) 
        { DepositRate = new MockRates().GetCurrentDepositRate(cur); }
    }
}