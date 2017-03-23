using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WFLCalc.UI
{
    internal class WFLDistanceConverter : IValueConverter
    {
        private int originalValue;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            originalValue = (int)(value ?? 0);
            return (originalValue / 10) / 100d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return originalValue;
        }
    }
}
