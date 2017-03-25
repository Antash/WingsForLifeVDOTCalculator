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
        }

        internal void SetDuration(TimeSpan duration)
        {
        }
    }
}