using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA
{
    public abstract class Account
    {
        public string AccountNumber { get; private set; }
        public decimal Money { get; set; }
        public Currency AccountCurrency { get; init; }
        public DateTime OpenDate { get; private set; }
        public Status AccountStatus { get; private set; }
        public enum Status { Reserved, Actual, Blocked, Closed, Undefined }
        public enum Currency { EUR=978, RUR=810, USD=840, CNY=156 }

        public static Dictionary<string,Client> AllBankAccounts { get; set; }
        static Account()
        {
            AllBankAccounts = new();
            index = 0;
        }  


        protected static int index=0;
        protected virtual string GetNewAccountNummber(Currency cur)
        {
            StringBuilder s = new StringBuilder(20);
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
}