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
        private T? _account;

        public Transaction(T account)
        {
            _account = account;
        }

        public bool AddMoney(/*T account, */ decimal amount)
        {
            if (_account == null) return false;
            _account.Money += amount;
            return true;
        }

        public bool CutMoney(T account, decimal amount) { return true; }
        public bool CloseAccount()
        {
            if (_account == null) return false;
            if (_account.Money != 0)
            {
                MessageBox.Show("Для закрытия счета баланс должен быть = 0");
                return false;
            }
            _account.AccountStatus = Account.Status.Closed;
            return true;
        }

        public bool TransferFromTo(T source, T destination, decimal amount)
        {
            //amount - сумма средств в валюте счета source
            MessageBox.Show(source.AccountCurrency.ToString());
            MessageBox.Show(destination.AccountCurrency.ToString());

            return true;
        }

    }
}
