using System;
using Android.Content;
using Android.Widget;
using Android.Util;
using Android.Runtime;

namespace WFLCalc.Droid
{
    [Register("WFLCalc.Droid.TimeDurationPicker")]
    public class TimeDurationPicker : FrameLayout
    {
        private NumberPicker hoursPicker;
        private NumberPicker minutesPicker;
        private NumberPicker secondsPicker;

        public TimeDurationPicker(Context context)
            : this(context , null)
        {

        }

        public TimeDurationPicker(Context context, IAttributeSet attrs)
            : this(context, attrs, Resource.Attribute.titleTextStyle)
        {

        }

        public TimeDurationPicker(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
            Inflate(context, Resource.Layout.time_duration_picker, this);

            hoursPicker = (NumberPicker)FindViewById(Resource.Id.hoursPicker);
            minutesPicker = (NumberPicker)FindViewById(Resource.Id.minutesPicker);
            secondsPicker = (NumberPicker)FindViewById(Resource.Id.secondsPicker);

            hoursPicker.MinValue = minutesPicker.MinValue = secondsPicker.MinValue = 0;
            hoursPicker.MaxValue = 23;
            minutesPicker.MaxValue = secondsPicker.MaxValue = 59;
        }

        internal void SetDuration(TimeSpan duration)
        {
            hoursPicker.Value = duration.Hours;
            minutesPicker.Value = duration.Minutes;
            secondsPicker.Value = duration.Seconds;
        }

        internal TimeSpan GetDuration()
        {
            return new TimeSpan(hoursPicker.Value, minutesPicker.Value, secondsPicker.Value);
        }
    }
}