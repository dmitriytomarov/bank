using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankA
{
    /// <summary>
    /// Конвертер валют. Возвращает double коэффициент на который надо умножить чтобы получить сумму во второй валюте из суммы первой
    /// </summary>
    internal interface IConverter
    {
        public double Convert(Account.Currency currFrom, Account.Currency currTo);
    }

    public class Converter : IConverter
    {
        public double Convert(Account.Currency currFrom, Account.Currency currTo)
        {
            if (currFrom != currTo)
            {
                MessageBox.Show("Реализация конвертера нужна. Возвращаю 1,0");
                return 1.0;
            }
            return 1.0;
        }
    }
}
