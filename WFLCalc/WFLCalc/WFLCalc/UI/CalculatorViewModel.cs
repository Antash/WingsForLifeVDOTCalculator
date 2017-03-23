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
                if (time.TotalSeconds > 0)
                {
                    DataEntered = true;
                    vDotCalculator = new VdotCalculator(distance.Meters, time);
                    OnPropertyChanged("SampleTime");
                    OnPropertyChanged("DataEntered");
                }
            }
        }

        public bool DataEntered { get; set; }
        public bool VdotExists { get; set; }
        public double Vdot
        {
            get; set;
        }

        public int WFLRunEstimatedDistance { get; set; }
        public TimeSpan WFLRunEstimatedTime { get; set; }
        public TimeSpan WFLRunEstimatedPace { get; set; }

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
                new DistanceSample(15000, "15K"),
                new DistanceSample(21100, Resx.AppResources.HMarathon_text),
                new DistanceSample(30000, "30K"),
                new DistanceSample(42195, Resx.AppResources.Marathon_text),
                new DistanceSample(100000, "100K"),
            };
            SelectedDistance = SampleDistances[2];
            vDotCalculator = new VdotCalculator(distance.Meters, time);
            IncreaseVdotCommand = new Command(() => ChangeVdot(1));
            DecreaseVdotCommand = new Command(() => ChangeVdot(-1));
            CalculateVdotCommand = new Command(CalculateVdot);
        }

        private void CalculateVdot(object obj)
        {
            VdotExists = true;
            Vdot = vDotCalculator.GetVdot();
            WFLRunEstimatedDistance = vDotCalculator.GetWingsForLifeEstimatedResult();
            WFLRunEstimatedTime = vDotCalculator.GetTime(WFLRunEstimatedDistance);
            WFLRunEstimatedPace = RunningUtils.GetPace(WFLRunEstimatedDistance, WFLRunEstimatedTime);
            OnPropertyChanged("Vdot");
            OnPropertyChanged("WFLRunEstimatedDistance");
            OnPropertyChanged("WFLRunEstimatedTime");
            OnPropertyChanged("WFLRunEstimatedPace");
            OnPropertyChanged("DataEntered");
            OnPropertyChanged("VdotExists");
        }

        private void ChangeVdot(double value)
        {
            if (Vdot + value < 1)
            {
                return;
            }
            vDotCalculator.Tune(value);

            Vdot = vDotCalculator.GetVdot();
            WFLRunEstimatedDistance = vDotCalculator.GetWingsForLifeEstimatedResult();
            WFLRunEstimatedTime = vDotCalculator.GetTime(WFLRunEstimatedDistance);
            WFLRunEstimatedPace = RunningUtils.GetPace(WFLRunEstimatedDistance, WFLRunEstimatedTime);
            SampleTime = vDotCalculator.GetTime(distance.Meters);
            OnPropertyChanged("Vdot");
            OnPropertyChanged("WFLRunEstimatedDistance");
            OnPropertyChanged("WFLRunEstimatedTime");
            OnPropertyChanged("WFLRunEstimatedPace");
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
