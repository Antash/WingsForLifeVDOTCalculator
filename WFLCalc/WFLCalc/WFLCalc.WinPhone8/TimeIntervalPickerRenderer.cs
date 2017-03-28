using Coding4Fun.Toolkit.Controls;
using System;
using System.Windows;
using WFLCalc.UI;
using WFLCalc.WinPhone8;
using Xamarin.Forms.Platform.WinPhone;
using System.ComponentModel;

[assembly: Xamarin.Forms.ExportRenderer(typeof(TimeIntervalPicker), typeof(TimeIntervalPickerRenderer))]

namespace WFLCalc.WinPhone8
{
    public class TimeIntervalPickerRenderer : ViewRenderer<TimeIntervalPicker, TimeSpanPicker>
    {
        private WFLCalc.UI.TimeIntervalPicker timeIntervalPicker;

        protected override void OnElementChanged(ElementChangedEventArgs<TimeIntervalPicker> e)
        {
            base.OnElementChanged(e);
            this.timeIntervalPicker = e.NewElement;
            var picker = new TimeSpanPicker();
            picker.SetValue(TimeSpanPicker.ValueProperty, timeIntervalPicker.SelectedTime);
            picker.ValueChanged += OnValueChanged;
            this.SetNativeControl(picker);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
                return;

            if (e.PropertyName == TimeIntervalPicker.SelectedIndexProperty.PropertyName)
            {
                var timeSpanPicker = (TimeSpanPicker)Control;

                timeSpanPicker.SetValue(TimeSpanPicker.ValueProperty, timeIntervalPicker.SelectedTime);
            }
        }

        private void OnValueChanged(object sender, RoutedPropertyChangedEventArgs<TimeSpan> e)
        {
            timeIntervalPicker.SetValue(TimeIntervalPicker.SelectedTimeProperty, e.NewValue);
        }
    }
}
