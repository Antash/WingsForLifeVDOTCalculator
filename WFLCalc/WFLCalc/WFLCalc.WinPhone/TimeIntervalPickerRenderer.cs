using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFLCalc.UI;
using WFLCalc.WinPhone;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinRT;

[assembly: ExportRenderer(typeof(TimeIntervalPicker), typeof(TimeIntervalPickerRenderer))]

namespace WFLCalc.WinPhone
{
    public class TimeIntervalPickerRenderer : ViewRenderer<TimeIntervalPicker, TextBox>
    {
        private const string DurationFormat = @"hh\:mm\:ss";
        //private DurationPicker dialog;
        private WFLCalc.UI.TimeIntervalPicker timeIntervalPicker;
        // Override the OnElementChanged method so we can tweak this renderer post-initial setup

        protected override void OnElementChanged(ElementChangedEventArgs<WFLCalc.UI.TimeIntervalPicker> e)
        {
            base.OnElementChanged(e);
            this.timeIntervalPicker = e.NewElement;
            this.SetNativeControl(new TextBox());
            this.Control.PointerPressed += Control_PointerPressed;
            this.Control.Text = TimeSpan.FromSeconds(0).ToString(DurationFormat);
            this.Control.GotFocus += Control_GotFocus; ;
        }

        private void Control_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            ShowTimePicker();
        }

        private void Control_GotFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //if (e.OriginalSource) { ShowTimePicker(); }
        }

        private void Control_ManipulationStarted(object sender, Windows.UI.Xaml.Input.ManipulationStartedRoutedEventArgs e)
        {
            ShowTimePicker();
        }

        private void ShowTimePicker()
        {
           // if (dialog == null)
            {
            //    dialog = new DurationPicker();//Forms.Context, this, TimeSpan.FromSeconds(0));
            }
            var p = new Popup();
            //p.Child = dialog;
            p.IsOpen = true;
            //dialog.();
        }
    }
}
