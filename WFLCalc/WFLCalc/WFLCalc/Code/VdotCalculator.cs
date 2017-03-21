using System;

namespace WFLCalc
{
    public class VdotCalculator : IVdotCalculator
    {
        private double vDot;

        public VdotCalculator(int distance, TimeSpan time)
        {
            int d = distance;
            double t = time.TotalMinutes;
            //c(t) or Oxygen Cost
            double c = -4.6 + .182258 * d / t + .000104 * d * d / t / t;
            //i(t) or % VO2 Max
            double i = .8 + .1894393 * Math.Exp(-.012778 * t) + .2989558 * Math.Exp(-.1932605 * t);
            vDot = Math.Round(1000 * c / i) / 1000;
        }

        public TimeSpan GetTime(int distance)
        {
            int d = distance;
            //start with a reasonable guess
            double t = d * .004;
            double v = vDot;
            double e;
            int n = 0;
            do
            {
                double c = -4.6 + .182258 * d / t + .000104 * d * d / t / t;       //c(t) or Oxygen Cost
                double i = .8 + .1894393 * Math.Exp(-.012778 * t) +        //i(t) or Intensity
                    .2989558 * Math.Exp(-.1932605 * t);
                e = Math.Abs(c - i * v);                //distance between curves
                double dc = -.182258 * d / t / t - 2 * .000104 * d * d / t / t / t;        //dc(t)/dt or slope of c(t) curve
                double di = -.012778 * .1894393 * Math.Exp(-.012778 * t) - //di(t)/dt or slope of i(t) curve
                        .1932605 * .2989558 * Math.Exp(-.1932605 * t);
                double dt = (c - i * v) / (dc - di * v);               //predicted correction
                t -= dt;                        //new t value to try
                n++;                        //times through loop
            }
            while (n < 10 && e > .1);                   //test for convergence
            return TimeSpan.FromMinutes(t);
        }

        public double GetVdot()
        {
            return vDot;
        }

        public int GetWingsForLifeEstimatedResult()
        {
            int distMin = 0, distMax = 200000;
            while (distMax - distMin > 10)
            {
                int distAvg = (distMin + distMax) / 2;
                if (WingsForLifeModel.GetTime(distAvg).CompareTo(GetTime(distAvg)) < 0)
                {
                    distMax = distAvg;
                }
                else
                {
                    distMin = distAvg;
                }
            }
            return distMin;
        }

        public void Tune(double value)
        {
            vDot += value;
        }
    }
}

