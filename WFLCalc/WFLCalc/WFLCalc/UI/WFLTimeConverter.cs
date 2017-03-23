using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFLCalc.Resx;
using Xamarin.Forms;

namespace WFLCalc.UI
{
    internal class WFLTimeConverter : IValueConverter
    {
        private TimeSpan time;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            time = (TimeSpan)value;
            return time.Hours == 0 
                ? string.Format(AppResources.Time_on_distance_m_text, time)
                : string.Format(AppResources.Time_on_distance_hm_text, time, 
                time.Hours > 1 ? AppResources.Hours_text : AppResources.Hour_text,
                time.Minutes > 1 ? AppResources.Minutes_text : AppResources.Minute_text);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return time;
        }
    }
}
