using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BankA
{
    internal class ConvertBoolVisibilityAddMoneyTextbox : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return Visibility.Visible;

            }
            else
            {
                return Visibility.Hidden;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool res;
            switch ((Visibility)value)
            {
                case Visibility.Visible:
                    res = true; break;
                default:
                    res = false; break;

            }
            return res;
        }
    }
}
