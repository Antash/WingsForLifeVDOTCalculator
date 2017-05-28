using System;
using System.ComponentModel;
using System.Windows.Input;
using WFLCalc.Resx;
using WFLCalc.Helpers;
using Xamarin.Forms;

namespace WFLCalc.UI
{
    internal class CalculatorViewModel : INotifyPropertyChanged
    {
        private IVdotCalculator vDotCalculator;
        private TimeSpan time;
        private DistanceSample distance;
        private DisplayUnit unit;

        public DistanceSample[] SampleDistances { get; } = new []
            {
                new DistanceSample(5000, "5K"),
                new DistanceSample(10000, "10K"),
                new DistanceSample(15000, "15K"),
                new DistanceSample(21100, AppResources.HMarathon_text),
                new DistanceSample(30000, "30K"),
                new DistanceSample(42195, AppResources.Marathon_text),
            };

        public DisplayUnit[] Units { get; } = new[] 
            {
                new DisplayUnit(Unit.Kilometers, AppResources.Kilometers_text),
                new DisplayUnit(Unit.Miles, AppResources.Miles_text)
            };
        
        public DisplayUnit SelectedUnit
        {
            get
            {
                return unit ?? Units[(int)Settings.CurrentUnits];
            }
            set
            {
                if (Settings.CurrentUnits != value.Unit)
                {
                    unit = value;
                    Settings.CurrentUnits = value.Unit;
                    OnPropertyChanged("SelectedUnit");
                    OnPropertyChanged("WFLRunEstimatedDistance");
                    OnPropertyChanged("WFLRunEstimatedPace");
                }
            }
        }

        public DistanceSample SelectedDistance
        {
            get
            {
                return distance;
            }
            set
            {
                if (distance != value)
                {
                    distance = value;
                    OnPropertyChanged("SelectedDistance");
                }
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
                if (value.TotalSeconds > 0 && time != value)
                {
                    time = value;
                    OnPropertyChanged("SampleTime");
                    DataEntered = true;
                    OnPropertyChanged("DataEntered");
                }
            }
        }

        public bool DataEntered { get; set; }

        public bool Initialized { get; set; }

        public bool VdotCanInc
        {
            get { return Vdot < VdotCalculator.MaxVdot && Vdot > 0; }
        }

        public bool VdotCanDec
        {
            get { return Vdot > VdotCalculator.MinVdot; }
        }

        public double Vdot { get; set; }

        public int WFLRunEstimatedDistance { get; set; }

        public TimeSpan WFLRunEstimatedTime { get; set; }

        public TimeSpan WFLRunEstimatedPace { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand IncreaseVdotCommand { get; protected set; }

        public ICommand DecreaseVdotCommand { get; protected set; }

        public ICommand CalculateVdotCommand { get; protected set; }

        public ICommand OpenVdotInfoCommand { get; protected set; }

        public ICommand OpenWflInfoCommand { get; protected set; }

        public ICommand ShareCommand { get; protected set; }

        public CalculatorViewModel()
        {
            SelectedDistance = SampleDistances[1];
            IncreaseVdotCommand = new Command(() => ChangeVdot(1));
            DecreaseVdotCommand = new Command(() => ChangeVdot(-1));
            CalculateVdotCommand = new Command(CalculateVdot);
            OpenVdotInfoCommand = new Command(() => { Device.OpenUri(new Uri(AppResources.Vdot_url)); });
            OpenWflInfoCommand = new Command(() => { Device.OpenUri(new Uri(AppResources.Wfl_url)); });

            var webImage = new Image { Aspect = Aspect.AspectFit };
            var source = ImageSource.FromUri(new Uri("https://xamarin.com/content/images/pages/forms/example-app.png"));
            ShareCommand = new Command(() => MessagingCenter.Send<ImageSource>(source, "Share"));
        }

        private void CalculateVdot(object obj)
        {
            vDotCalculator = new VdotCalculator(distance.Meters, time);
            Vdot = vDotCalculator.GetVdot();
            WFLRunEstimatedDistance = vDotCalculator.GetWingsForLifeEstimatedResult();
            WFLRunEstimatedTime = WingsForLifeModel.GetTime(WFLRunEstimatedDistance);
            WFLRunEstimatedPace = RunningUtils.GetPace(WFLRunEstimatedDistance, WFLRunEstimatedTime, Settings.CurrentUnits);
            Initialized = true;
            OnPropertyChanged("Vdot");
            OnPropertyChanged("WFLRunEstimatedDistance");
            OnPropertyChanged("WFLRunEstimatedTime");
            OnPropertyChanged("WFLRunEstimatedPace");
            OnPropertyChanged("Initialized");
            OnPropertyChanged("VdotCanDec");
            OnPropertyChanged("VdotCanInc");
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
            WFLRunEstimatedTime = WingsForLifeModel.GetTime(WFLRunEstimatedDistance);
            WFLRunEstimatedPace = RunningUtils.GetPace(WFLRunEstimatedDistance, WFLRunEstimatedTime, Settings.CurrentUnits);
            SampleTime = vDotCalculator.GetTime(distance.Meters);
            OnPropertyChanged("Vdot");
            OnPropertyChanged("WFLRunEstimatedDistance");
            OnPropertyChanged("WFLRunEstimatedTime");
            OnPropertyChanged("WFLRunEstimatedPace");
            OnPropertyChanged("VdotCanDec");
            OnPropertyChanged("VdotCanInc");
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
