
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace WFLCalc.Droid
{
    [Activity (Label = "WingsForLife VDOT Calculator", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/splashscreen", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(global::Android.Resource.Style.ThemeDeviceDefault);
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }
	}
}


