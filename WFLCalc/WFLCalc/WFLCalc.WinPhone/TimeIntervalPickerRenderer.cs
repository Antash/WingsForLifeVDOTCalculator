using System;
using WFLCalc.UI;
using WFLCalc.WinPhone;
using Windows.UI.Xaml;
using Xamarin.Forms.Platform.WinRT;

[assembly: ExportRenderer(typeof(TimeIntervalPicker), typeof(TimeIntervalPickerRenderer))]

namespace WFLCalc.WinPhone
{
    public class TimeIntervalPickerRenderer : ViewRenderer<TimeIntervalPicker, Windows.UI.Xaml.Controls.Button>
    {
        private const string DurationFormat = @"hh\:mm\:ss";
        private WFLCalc.UI.TimeIntervalPicker timeIntervalPicker;

        // Override the OnElementChanged method so we can tweak this renderer post-initial setup
        protected override void OnElementChanged(ElementChangedEventArgs<WFLCalc.UI.TimeIntervalPicker> e)
        {
            base.OnElementChanged(e);
            this.timeIntervalPicker = e.NewElement;
            var btn = new Windows.UI.Xaml.Controls.Button();
            btn.Content = TimeSpan.FromSeconds(0).ToString(DurationFormat);
            btn.Click += OnBtnClick;
            this.SetNativeControl(btn);
        }

        private void OnBtnClick(object sender, RoutedEventArgs e)
        {
            ShowTimePicker();
        }

        private void ShowTimePicker()
        {
            var frame = Window.Current.Content as Windows.UI.Xaml.Controls.Frame;
            frame.Navigate(typeof(DurationPickerPage), timeIntervalPicker.SelectedTime);
        }
    }
}
