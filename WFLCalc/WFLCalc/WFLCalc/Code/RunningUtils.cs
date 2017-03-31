using System;

namespace WFLCalc
{
    public static class RunningUtils
    {
        public static double ConvertToMiles(this double kilometers)
        {
            return kilometers / 1.60934;
        }

        public static TimeSpan GetPace(int distance, TimeSpan time, Unit unit = Unit.Kilometers)
        {
            double d = unit == Unit.Miles ? (distance / 1000d).ConvertToMiles() : (distance / 1000d);
            return TimeSpan.FromSeconds(Math.Round(time.TotalSeconds / d)).StripMilliseconds();
        }

        public static TimeSpan StripMilliseconds(this TimeSpan time)
        {
            return new TimeSpan(time.Days, time.Hours, time.Minutes, time.Seconds);
        }

        public static bool HasValue(this double value)
        {
            return !double.IsNaN(value) && !double.IsInfinity(value);
        }
    }
}
