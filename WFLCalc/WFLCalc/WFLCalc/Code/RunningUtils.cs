using System;

namespace WFLCalc
{
    public static class RunningUtils
    {
        public static TimeSpan GetPace(int distance, TimeSpan time)
        {
            return TimeSpan.FromSeconds(Math.Round(time.TotalSeconds / (distance / 1000d))).StripMilliseconds();
        }

        public static TimeSpan StripMilliseconds(this TimeSpan time)
        {
            return new TimeSpan(time.Days, time.Hours, time.Minutes, time.Seconds);
        }
    }
}
