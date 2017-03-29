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
            return string.Format(AppResources.Time_on_distance_text,
                time.Hours == 0 
                ? string.Format(AppResources.Duration_m_text, time)
                : string.Format(AppResources.Duration_hm_text, time)); 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return time;
        }
    }
}
