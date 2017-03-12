using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFLCalc
{
    internal static class WingsForLifeModel
    {
        private static readonly double[] speed =
            {
                15d * 1000 / 3600,
                16d * 1000 / 3600,
                17d * 1000 / 3600,
                20d * 1000 / 3600,
                35d * 1000 / 3600
            };

        private static readonly int[] time =
            {
                60 * 60,
                60 * 60,
                60 * 60,
                120 * 60,
                int.MaxValue
            };

        public static TimeSpan GetTime(int distance)
        {
            int sTime = 30 * 60;
            int i = 0;
            do
            {
                var carDistance = speed[i] * time[i];
                sTime += distance < carDistance ? (int)Math.Round(distance / speed[i]) : time[i];
                distance -= (int)Math.Round(carDistance);
                i++;
            } while (distance > 0);
            return TimeSpan.FromSeconds(sTime);
        }
    }
}
