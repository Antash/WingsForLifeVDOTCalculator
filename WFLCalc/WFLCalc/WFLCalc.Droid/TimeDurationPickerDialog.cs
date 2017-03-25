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

namespace WFLCalc.Droid
{
    public class TimeDurationPickerDialog : AlertDialog, IDialogInterfaceOnClickListener
    {
        private IOnTimeSetListener timeSetListner;
        private TimeSpan duration;
        private TimeDurationPicker durationInputView;

        public TimeDurationPickerDialog(Context context, IOnTimeSetListener listner, int hours, int minutes, int seconds)
            : base(context)
        {
            this.timeSetListner = listner;
            this.duration = new TimeSpan(hours, minutes, seconds);
            LayoutInflater inflater = LayoutInflater.From(context);
            View view = inflater.Inflate(Resource.Layout.time_duration_picker_dialog, null);
            durationInputView = (TimeDurationPicker)view;
            durationInputView.SetDuration(duration);

            SetView(view);
            SetCancelable(true);
            SetButton((int)DialogButtonType.Positive, "Ok", this);
            SetButton((int)DialogButtonType.Negative, "Cancel", this);
        }

        public interface IOnTimeSetListener
        {
            void OnTimeSet(TimeDurationPickerDialog view, int hour, int minute, int second);
        }

        public void OnClick(IDialogInterface dialog, int which)
        {
            throw new NotImplementedException();
        }
    }
}