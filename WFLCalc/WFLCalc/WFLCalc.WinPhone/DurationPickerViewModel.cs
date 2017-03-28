using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WFLCalc.WinPhone
{
    internal class DurationPickerViewModel
    {
        public int[] Hours { get; private set; } = new int[25];
        public int[] Minutes { get; private set; } = new int[60];
        public int[] Seconds { get; private set; } = new int[60];

        public TimeSpan SelectedTime { get; set; }

        public ICommand AcceptCommand { protected set; get; }

        public ICommand CancelCommand { protected set; get; }

        public DurationPickerViewModel()
        {
            for (int i = 0; i <= 24; i++)
            {
                Hours[i] = i;
            }
            for (int i = 0; i <= 59; i++)
            {
                Minutes[i] = Seconds[i] = i;
            }
            //AcceptCommand = new 
            //CancelCommand = new Command(() => ChangeVdot(-1));
        }
    }
}
