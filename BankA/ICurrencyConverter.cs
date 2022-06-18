using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankA
{
    /// <summary>
    /// Конвертер валют. Возвращает decmal коэффициент на который надо умножить чтобы получить сумму во второй валюте из суммы первой
    /// </summary>
    internal interface ICurrencyConverter
    {
        public  decimal Convert(Account.Currency currFrom, Account.Currency currTo);
    }

    public class MockConverter : ICurrencyConverter
    {
        public decimal Convert(Account.Currency currFrom, Account.Currency currTo)
        {

            if (currFrom == currTo) return 1m;

            Dictionary<string,decimal> rates = new();

            rates.Add("EURRUR", 59.33m);
            rates.Add("EURUSD", 1.05m);
            rates.Add("EURCNY", 7.03m);
            rates.Add("USDRUR", 56.71m);
            rates.Add("USDEUR", 0.95247m);
            rates.Add("USDCNY", 6.63m);
            rates.Add("RURUSD", 0.017634m);
            rates.Add("RUREUR", 0.016855m);
            rates.Add("RURCNY", 0.11685m);
            rates.Add("CNYRUR", 8.56m);
            rates.Add("CNYEUR", 0.14223m);
            rates.Add("CNYUSD", 0.15091m);

            string s = currFrom.ToString() + currTo.ToString();

            return rates[s];
        }
    }
}
