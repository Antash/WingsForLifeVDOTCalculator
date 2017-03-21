using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace WFLCalc.UI
{
    internal class CalculatorViewModel : INotifyPropertyChanged
    {
        public struct DistanceSample
        {
            public int Meters { get; private set; }
            public string Display { get; private set; }

            public DistanceSample(int distance, string displayValue)
            {
                Meters = distance;
                Display = displayValue;
            }
        }

        private IVdotCalculator vDotCalculator;
        private TimeSpan time;
        private DistanceSample distance;

        public IList<DistanceSample> SampleDistances { get; private set; }

        public DistanceSample SelectedDistance
        {
            get
            {
                return distance;
            }
            set
            {
                distance = value;
                vDotCalculator = new VdotCalculator(distance.Meters, time);
                OnPropertyChanged("SelectedDistance");
            }
        }

        public TimeSpan SampleTime
        { 
            get
            {
                return time;
            }
            set
            {
                time = value;
                vDotCalculator = new VdotCalculator(distance.Meters, time);
                OnPropertyChanged("SampleTime");
            }
        }

        public double Vdot { get; set; }
        public double WFLRunEstimationDistance { get; set; }
        public TimeSpan WFLRunEstimationTime { get; set; }
        public TimeSpan WFLRunEstimationPace { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand IncreaseVdotCommand { protected set; get; }

        public ICommand DecreaseVdotCommand { protected set; get; }

        public ICommand CalculateVdotCommand { protected set; get; }

        public CalculatorViewModel()
        {
            SampleDistances = new List<DistanceSample>
            {
                new DistanceSample(3000, "3K"),
                new DistanceSample(5000, "5K"),
                new DistanceSample(10000, "10K"),
                new DistanceSample(21100, "Half Marathon"),
                new DistanceSample(42195, "Marathon"),
            };
            SelectedDistance = SampleDistances[0];
            vDotCalculator = new VdotCalculator(distance.Meters, time);
            IncreaseVdotCommand = new Command(() => ChangeVdot(1));
            DecreaseVdotCommand = new Command(() => ChangeVdot(-1));
            CalculateVdotCommand = new Command(CalculateVdot);
        }

        private void CalculateVdot(object obj)
        {
            Vdot = vDotCalculator.GetVdot();
            WFLRunEstimationDistance = vDotCalculator.GetWingsForLifeEstimatedResult();
            OnPropertyChanged("Vdot");
            OnPropertyChanged("WFLRunEstimationDistance");
        }

        private void ChangeVdot(double value)
        {
            vDotCalculator.Tune(value);

            Vdot = vDotCalculator.GetVdot();
            WFLRunEstimationDistance = vDotCalculator.GetWingsForLifeEstimatedResult();
            SampleTime = vDotCalculator.GetTime(distance.Meters);
            OnPropertyChanged("Vdot");
            OnPropertyChanged("WFLRunEstimationDistance");
            OnPropertyChanged("SampleTime");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
