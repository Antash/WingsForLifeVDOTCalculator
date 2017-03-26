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
        private const string Duration = "duration";

        public interface IOnDurationSetListener
        {
            void OnDurationSet(TimeSpan duration);
        }

        private IOnDurationSetListener durationSetListener;
        private TimeSpan duration;
        private TimeDurationPicker durationInputView;

        public TimeDurationPickerDialog(Context context, IOnDurationSetListener listner, TimeSpan defaultDuration)
            : base(context)
        {
            this.durationSetListener = listner;
            this.duration = defaultDuration;
            LayoutInflater inflater = LayoutInflater.From(context);
            View view = inflater.Inflate(Resource.Layout.time_duration_picker_dialog, null);
            durationInputView = (TimeDurationPicker)view;
            durationInputView.SetDuration(duration);

            SetView(view);
            SetCancelable(true);
            SetButton((int)DialogButtonType.Positive, "Ok", this);
            SetButton((int)DialogButtonType.Negative, "Cancel", this);
        }

        public void OnClick(IDialogInterface dialog, int which)
        {
            switch (which)
            {
                case (int)DialogButtonType.Positive:
                    if (durationSetListener != null)
                    {
                        durationSetListener.OnDurationSet(durationInputView.GetDuration());
                    }
                    break;
                case (int)DialogButtonType.Negative:
                    Cancel();
                    break;
            }
        }

        public void SetDuration(TimeSpan newDuration)
        {
            duration = newDuration;
            durationInputView.SetDuration(duration);
        }

        public override Bundle OnSaveInstanceState()
        {
            Bundle state = base.OnSaveInstanceState();
            state.PutInt(Duration, (int)Math.Ceiling(durationInputView.GetDuration().TotalSeconds));
            return state;
        }

        public override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            int duration = savedInstanceState.GetInt(Duration);
            durationInputView.SetDuration(TimeSpan.FromSeconds(duration));
        }
    }
}