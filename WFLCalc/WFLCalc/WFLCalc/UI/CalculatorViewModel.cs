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
        private IVdotCalculator vDotCalculator;
        private TimeSpan time;
        private double distance;

        public IList<double> SampleDistances { get; private set; }

        public double SelectedDistance
        {
            get
            {
                return distance;
            }
            set
            {
                distance = value;
                vDotCalculator = new VdotCalculator((int)(distance * 1000), time);
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
                vDotCalculator = new VdotCalculator((int)(distance * 1000), time);
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
            SampleDistances = new List<double> { 3, 5, 10, 21.1, 42.2 };
            vDotCalculator = new VdotCalculator((int)(distance * 1000), time);
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
            OnPropertyChanged("Vdot");
            OnPropertyChanged("WFLRunEstimationDistance");
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
