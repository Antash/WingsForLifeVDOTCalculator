using System;

namespace WFLCalc
{
    public interface IVdotCalculator
    {
        double GetVdot();
        TimeSpan GetTime(int distance);
        int GetWingsForLifeEstimatedResult();
        void Tune(double value);
    }
}
