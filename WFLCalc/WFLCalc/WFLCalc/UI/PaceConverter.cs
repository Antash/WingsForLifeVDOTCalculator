using System;
using System.Globalization;
using WFLCalc.Resx;
using WFLCalc.Helpers;
using Xamarin.Forms;

namespace WFLCalc.UI
{
    internal class PaceConverter : IValueConverter
    {
        private TimeSpan originalValue;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var units = Settings.CurrentUnits;
            originalValue = (TimeSpan)value;
            return string.Format(units == Unit.Miles ? AppResources.Pace_miles_text : AppResources.Pace_text, originalValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return originalValue;
        }
    }
}
