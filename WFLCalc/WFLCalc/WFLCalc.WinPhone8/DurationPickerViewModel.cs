using Microsoft.Phone.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;

namespace WFLCalc.WinPhone8
{
    abstract class DataSource : ILoopingSelectorDataSource
    {
        private TimeSpanWrapper _selectedItem;

        public object GetNext(object relativeTo)
        {
            TimeSpan? next = GetRelativeTo(((TimeSpanWrapper)relativeTo).TimeSpan, 1);
            return next.HasValue ? new TimeSpanWrapper(next.Value) : null;
        }

        public object GetPrevious(object relativeTo)
        {
            TimeSpan? next = GetRelativeTo(((TimeSpanWrapper)relativeTo).TimeSpan, -1);
            return next.HasValue ? new TimeSpanWrapper(next.Value) : null;
        }

        protected abstract TimeSpan? GetRelativeTo(TimeSpan relativeDate, int delta);

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value != _selectedItem)
                {
                    TimeSpanWrapper valueWrapper = (TimeSpanWrapper)value;
                    if ((null == valueWrapper) || (null == _selectedItem) || (valueWrapper.TimeSpan != _selectedItem.TimeSpan))
                    {
                        object previousSelectedItem = _selectedItem;
                        _selectedItem = valueWrapper;
                        var handler = SelectionChanged;
                        if (null != handler)
                        {
                            handler(this, new SelectionChangedEventArgs(new object[] { previousSelectedItem }, new object[] { _selectedItem }));
                        }
                    }
                }
            }
        }

        public event EventHandler<SelectionChangedEventArgs> SelectionChanged;
    }

    class MinuteDataSource : DataSource
    {
        protected override TimeSpan? GetRelativeTo(TimeSpan relativeDate, int delta)
        {
            int minutesInHour = 60;
            int nextMinute = (minutesInHour + relativeDate.Minutes + delta) % minutesInHour;
            return new TimeSpan(relativeDate.Hours, nextMinute, relativeDate.Seconds);
        }
    }
    class SecondsDataSource : DataSource
    {
        protected override TimeSpan? GetRelativeTo(TimeSpan relativeDate, int delta)
        {
            int secondsInMinute = 60;
            int nextSecond = (secondsInMinute + relativeDate.Seconds + delta) % secondsInMinute;
            return new TimeSpan(relativeDate.Hours, relativeDate.Minutes, nextSecond);
        }
    }

    class TwentyFourHourDataSource : DataSource
    {
        protected override TimeSpan? GetRelativeTo(TimeSpan relativeDate, int delta)
        {
            int hoursInDay = 24;
            int nextHour = (hoursInDay + relativeDate.Hours + delta) % hoursInDay;
            return new TimeSpan(nextHour, relativeDate.Minutes, relativeDate.Seconds);
        }
    }
}
