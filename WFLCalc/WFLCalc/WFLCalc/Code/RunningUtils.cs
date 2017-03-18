using System;

namespace WFLCalc
{
    public static class RunningUtils
    {
        public static TimeSpan GetPace(int distance, TimeSpan time)
        {
            return TimeSpan.FromSeconds(Math.Round(time.TotalSeconds / (distance / 1000d)));
        }
    }
}
