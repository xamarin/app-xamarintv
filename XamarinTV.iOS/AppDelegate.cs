using FFImageLoading.Forms.Platform;
using Foundation;
using Sharpnado.Presentation.Forms.iOS;
using UIKit;

namespace XamarinTV.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();
            CachedImageRenderer.Init();
            SharpnadoInitializer.Initialize();
            LoadApplication(new App());

            return base.FinishedLaunching(application, launchOptions);
        }
    }
}


