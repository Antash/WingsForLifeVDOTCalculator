
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Net;
using Android.OS;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

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

            MessagingCenter.Subscribe<ImageSource>(this, "Share", Share, null);

            LoadApplication(new App());
        }

        private async void Share(ImageSource imageSource)
        {
            var intent = new Intent(Intent.ActionSend);
            intent.SetType("image/png");

            var handler = new ImageLoaderSourceHandler();
            var bitmap = await handler.LoadImageAsync(imageSource, this);

            var path = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDownloads
                + Java.IO.File.Separator + "logo.png");

            using (var os = new System.IO.FileStream(path.AbsolutePath, System.IO.FileMode.Create))
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, os);
            }

            intent.PutExtra(Intent.ExtraStream, Uri.FromFile(path));

            var intentChooser = Intent.CreateChooser(intent, "Share via");

            StartActivityForResult(intentChooser, 1);
        }
    }
}


