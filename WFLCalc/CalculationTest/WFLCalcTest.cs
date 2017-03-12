using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WFLCalc;

namespace CalculationTest
{
    [TestClass]
    public class WFLCalcTest
    {
        [TestMethod]
        public void TestVDot()
        {
            var vdot = new VdotCalculator(10000, new TimeSpan(0, 30, 0));
            Assert.AreEqual(72.803, vdot.GetVdot());
        }

        [TestMethod]
        public void TestWFL()
        {
            Assert.AreEqual(new TimeSpan(1, 30, 0), WingsForLifeModel.GetTime(15000));
            Assert.AreEqual(new TimeSpan(3, 36, 0), WingsForLifeModel.GetTime(50000));
            Assert.AreEqual(new TimeSpan(0, 30, 0), WingsForLifeModel.GetTime(0));
        }

        [TestMethod]
        public void TestWFLVdot()
        {
            var vdot = new VdotCalculator(10000, new TimeSpan(0, 30, 0));
            Assert.AreEqual(107390, vdot.GetWingsForLifeEstimatedResult());
            vdot = new VdotCalculator(42200, new TimeSpan(2, 49, 3));
            Assert.AreEqual(62066, vdot.GetWingsForLifeEstimatedResult());
        }
    }
}
