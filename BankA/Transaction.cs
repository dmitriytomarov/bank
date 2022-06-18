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
            //amount - сумма средств в валюте исходного счета source
            if (source.Money<amount)
            {
                MessageBox.Show($"Недостаточно средств на счете {source.AccountNumber}:  {source.Money} {source.AccountCurrency}");
                return false;
            }
            source.Money -= amount;
            destination.Money += ConvertAmount(source.AccountCurrency, destination.AccountCurrency, amount);
            return true;
        }

        public static decimal ConvertAmount(Account.Currency currFrom, Account.Currency currTo, decimal amount)
        {
            if (currFrom == currTo) return amount;
            ICurrencyConverter converter = new MockConverter();

            return Math.Round(amount * converter.Convert(currFrom,currTo), 2);

        }

    }
}
