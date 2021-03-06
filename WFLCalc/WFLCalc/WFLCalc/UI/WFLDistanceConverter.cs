﻿using System;
using System.Globalization;
using WFLCalc.Resx;
using WFLCalc.Helpers;
using Xamarin.Forms;

namespace WFLCalc.UI
{
    internal class WFLDistanceConverter : IValueConverter
    {
        private int originalValue;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var units = Settings.CurrentUnits;
            originalValue = (int)(value ?? 0);
            double d = units == Unit.Miles ? (originalValue / 1000d).ConvertToMiles()
                : originalValue / 1000d;
            return string.Format(units == Unit.Miles ? AppResources.Mile_text : AppResources.Km_text, Math.Round(d, 2));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return originalValue;
        }
    }
}
