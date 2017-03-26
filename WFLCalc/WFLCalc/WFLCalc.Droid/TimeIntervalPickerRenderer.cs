using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WFLCalc.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WFLCalc.UI.TimeIntervalPicker), typeof(TimeIntervalPickerRenderer))]

namespace WFLCalc.Droid
{
    public class TimeIntervalPickerRenderer : ViewRenderer<WFLCalc.UI.TimeIntervalPicker, global::Android.Widget.EditText>,
        TimeDurationPickerDialog.IOnDurationSetListener, 
        IJavaObject, 
        IDisposable
    {
        private const string DurationFormat = @"hh\:mm\:ss";
        private TimeDurationPickerDialog dialog;
        private WFLCalc.UI.TimeIntervalPicker timeIntervalPicker;

        protected override void OnElementChanged(ElementChangedEventArgs<WFLCalc.UI.TimeIntervalPicker> e)
        {
            base.OnElementChanged(e);
            this.timeIntervalPicker = e.NewElement;
            this.SetNativeControl(new global::Android.Widget.EditText(Forms.Context));
            this.Control.Click += Control_Click;
            this.Control.Text = TimeSpan.FromSeconds(0).ToString(DurationFormat);
            this.Control.KeyListener = null;
            this.Control.FocusChange += Control_FocusChange;
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
                return;

            if (e.PropertyName == Picker.SelectedIndexProperty.PropertyName)
            {
                dialog.SetDuration(timeIntervalPicker.SelectedTime);
                OnDurationSet(timeIntervalPicker.SelectedTime);
            }
        }

        void Control_FocusChange(object sender, global::Android.Views.View.FocusChangeEventArgs e)
        {
            if (e.HasFocus) { ShowTimePicker(); }
        }

        void Control_Click(object sender, EventArgs e)
        {
            ShowTimePicker();
        }

        private void ShowTimePicker()
        {
            if (dialog == null)
            {
                dialog = new TimeDurationPickerDialog(Forms.Context, this, TimeSpan.FromSeconds(0));
            }

            dialog.Show();
        }

        public void OnDurationSet(TimeSpan duration)
        {
            this.Element.SetValue(WFLCalc.UI.TimeIntervalPicker.SelectedTimeProperty, duration);
            this.Control.Text = duration.ToString(DurationFormat);
        }
    }
}