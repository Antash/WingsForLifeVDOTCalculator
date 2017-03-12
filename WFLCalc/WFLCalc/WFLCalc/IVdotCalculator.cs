using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFLCalc
{
    public interface IVdotCalculator
    {
        double GetVdot();
        TimeSpan GetTime(int distance);
        int GetWingsForLifeEstimatedResult();
    }
}
