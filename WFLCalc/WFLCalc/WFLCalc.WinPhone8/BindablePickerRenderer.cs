using FreshEssentials;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFLCalc.WinPhone8;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(BindablePicker), typeof(BindablePickerRenderer))]

namespace WFLCalc.WinPhone8
{
    public class BindablePickerRenderer : ViewRenderer<BindablePicker, ListPicker>
    {
        private BindablePicker picker;

        protected override void OnElementChanged(ElementChangedEventArgs<BindablePicker> e)
        {
            base.OnElementChanged(e);
            this.picker = e.NewElement;
            var listPicker = new ListPicker();
            listPicker.ItemsSource = new[] { 1, 2, 4, 7, 6 };//picker.ItemsSource;
            //listPicker.ItemTemplate = 
            listPicker.SelectedItem = 1;// picker.SelectedItem;
            //listPicker.FullModeItemTemplate;
            this.SetNativeControl(listPicker);
        }
    }
}
