namespace WFLCalc.UI
{
    public class DisplayUnit
    {
        public Unit Unit { get; private set; }
        public string Display { get; private set; }

        public DisplayUnit(Unit unit, string displayValue)
        {
            Unit = unit;
            Display = displayValue;
        }

        public override string ToString()
        {
            return Display;
        }

        public static bool operator ==(DisplayUnit obj1, DisplayUnit obj2)
        {
            if (obj1 == null || obj2 == null)
            {
                return false;
            }
            return obj1.Unit == obj2.Unit;
        }

        public static bool operator !=(DisplayUnit obj1, DisplayUnit obj2)
        {
            return !(obj1 == obj2);
        }
    }
}
