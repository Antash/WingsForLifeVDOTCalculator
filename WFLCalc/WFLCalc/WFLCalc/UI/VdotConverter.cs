﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace WFLCalc.UI
{
    internal class VdotConverter : IValueConverter
    {
        private double originalValue;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            originalValue = (double)value;
            return Math.Round(originalValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return originalValue;
        }
    }
}
