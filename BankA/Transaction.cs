using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankA
{
    internal class Transaction<T>
        where T : Account
    {
        public  T? Target { get; init; }
        public Transaction(T account)
        {
            Target = account;
        }

        public bool AddMoney(T account, decimal amount)
        {
            if (account == null) return false;
            account.Money += amount;
            return true;
        }

        public bool CutMoney(T account, decimal amount) { return true; }
        public bool CloseAccount(T account) { return true; }
        public bool TransferFromTo(T sourse, T destination, decimal amount)
        {
            return true;
        }

    }
}
