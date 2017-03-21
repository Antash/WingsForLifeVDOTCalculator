using Android.App;
using System;
using WFLCalc.Droid;
using WFLCalc.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(TimeIntervalPicker), typeof(TimeIntervalPickerRenderer))]
namespace WFLCalc.Droid
{
    public class TimeIntervalPickerRenderer : ViewRenderer<TimeIntervalPicker, global::Android.Widget.EditText>, TimePickerDialog.IOnTimeSetListener, IDisposable
    {
        private TimePickerDialog dialog = null;

        protected override void OnElementChanged(ElementChangedEventArgs<TimeIntervalPicker> e)
        {
            base.OnElementChanged(e);
            this.SetNativeControl(new global::Android.Widget.EditText(Forms.Context));
            this.Control.Click += Control_Click;
            this.Control.Text = DateTime.Now.ToString("HH:mm");
            this.Control.KeyListener = null;
            this.Control.FocusChange += Control_FocusChange;
        }

        void Control_FocusChange(object sender, global::Android.Views.View.FocusChangeEventArgs e)
        {
            if (e.HasFocus)
                ShowTimePicker();
        }

        void Control_Click(object sender, EventArgs e)
        {
            ShowTimePicker();
        }

        private void ShowTimePicker()
        {
            if (dialog == null)
            {
                dialog = new TimePickerDialog(Forms.Context, this, DateTime.Now.Hour, DateTime.Now.Minute, true);
            }

            dialog.Show();
        }

        public void OnTimeSet(global::Android.Widget.TimePicker view, int hourOfDay, int minute)
        {
            var time = new TimeSpan(hourOfDay, minute, 0);
            this.Element.SetValue(TimePicker.TimeProperty, time);

            this.Control.Text = time.ToString(@"hh\:mm");
        }
    }
}