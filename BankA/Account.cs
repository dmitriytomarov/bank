using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA
{
    abstract class Account
    {
        public string AccountNumber { get; private set; }
        public decimal Money { get; set; }
        public Currency AccountCurrency { get; init; }
        public DateTime OpenDate { get; private set; }
        public Status AccountStatus { get; private set; }
        public enum Status { Actual, Blocked, Closed, Undefined }
        public enum Currency { EUR, RUR, USD, CNY }

        public abstract bool AddMoney();
        public abstract bool CutMoney();

        public Account(string number, Currency cur)
        {
            AccountNumber = number;
            AccountCurrency = cur;
            Money = 0;
            OpenDate = DateTime.Now;
            AccountStatus = Status.Actual;
        }
        
    }
}
