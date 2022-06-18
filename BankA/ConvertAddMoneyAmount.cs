using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BankA
{
    internal class ConvertAddMoneyAmount : IValueConverter
    {
        //Regex regex = new Regex(@"\w");
        //Regex regex = new Regex(@"\d*.\d*");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string sFull = (string)value;  //полная строка 
            //sFull = Regex.Replace(sFull, @"^[a-zA-Z0-9]+$", "1"); return sFull;

            if (String.IsNullOrEmpty(sFull)) return "";
            string sLast = sFull[^1..];   //https://docs.microsoft.com/ru-ru/dotnet/csharp/whats-new/tutorials/ranges-indexes
            var sTrimmed = sFull[0..^1]; //подстрока кроме последнего символа

            if (sLast == "." || sLast == ",")  // если ввели точку но она уже есть
            {
                if (sTrimmed.Contains(',')) { return sTrimmed; } else { return sTrimmed + ","; }
            }

            char c = System.Convert.ToChar(sLast);
            if (c < 48 || c > 57) { return sTrimmed; }

            var a = sTrimmed.IndexOf('.');
            if (sTrimmed.Contains(',') && (sTrimmed.Length - sTrimmed.IndexOf(',')) >= 3)
            {
                return sTrimmed;
            }

            for (int i = 0; i < sFull.Length; i++)
            {
                c = (char)sFull[i];
                if (c <= 43 || c > 57) sFull = sFull.Replace( c.ToString(), "");
            }
            return sFull;
        }
    }
}
