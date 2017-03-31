namespace WFLCalc.UI
{
    public class DistanceSample
    {
        public int Meters { get; private set; }
        public string Display { get; private set; }

        public DistanceSample(int distance, string displayValue)
        {
            Meters = distance;
            Display = displayValue;
        }

        public override string ToString()
        {
            return Display;
        }

        public static bool operator ==(DistanceSample obj1, DistanceSample obj2)
        {
            if (obj1 == null || obj2 == null)
            {
                return false;
            }
            return obj1.Meters == obj2.Meters;
        }

        public static bool operator !=(DistanceSample obj1, DistanceSample obj2)
        {
            return !(obj1 == obj2);
        }
    }
}
