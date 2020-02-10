using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using FFImageLoading.Forms.Platform;
using Xamarin.Forms.DualScreen;
using Sharpnado.Presentation.Forms.Droid;

namespace XamarinTV.Droid
{
    [Activity(
        Label = "XamarinTV",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            DualScreenService.Init(this);
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            SetStatusBarColor(global::Android.Graphics.Color.Black);

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.SetFlags("IndicatorView_Experimental", "CarouselView_Experimental", "MediaElement_Experimental");
            Forms.Init(this, savedInstanceState); 
            CachedImageRenderer.Init(true);
            SharpnadoInitializer.Initialize();
            LoadApplication(new App());
            
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }      
    }
}