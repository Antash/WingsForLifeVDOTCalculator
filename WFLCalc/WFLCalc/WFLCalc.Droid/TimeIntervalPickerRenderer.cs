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
        TimeDurationPickerDialog.IOnTimeSetListener, 
        IJavaObject, 
        IDisposable
    {
        private const string DurationFormat = @"hh\:mm\:ss";
        private TimeDurationPickerDialog dialog = null;

        protected override void OnElementChanged(ElementChangedEventArgs<WFLCalc.UI.TimeIntervalPicker> e)
        {
            base.OnElementChanged(e);
            this.SetNativeControl(new global::Android.Widget.EditText(Forms.Context));
            this.Control.Click += Control_Click;
            this.Control.Text = TimeSpan.FromSeconds(0).ToString(DurationFormat);
            this.Control.KeyListener = null;
            this.Control.FocusChange += Control_FocusChange;
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
                dialog = new TimeDurationPickerDialog(Forms.Context, this, 0, 0, 0);
            }

            dialog.Show();
        }

        public void OnTimeSet(TimeDurationPickerDialog view, int hour, int minute, int second)
        {
            var time = new TimeSpan(hour, minute, second);
            this.Element.SetValue(WFLCalc.UI.TimeIntervalPicker.SelectedTimeProperty, time);
            this.Control.Text = time.ToString(DurationFormat);
        }
    }
}