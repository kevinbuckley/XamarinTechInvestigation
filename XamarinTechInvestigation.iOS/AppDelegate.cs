using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.SimpleNotificationService;
using Amazon;
using Amazon.CognitoIdentity;
using Foundation;
using UIKit;
using System.Runtime.InteropServices;
using ObjCRuntime;
using UserNotifications;


namespace XamarinTechInvestigation.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private Notifications.RemoteNotification noties = new Notifications.RemoteNotification();
       
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif
            global::Xamarin.Forms.Forms.Init();

            UNUserNotificationCenter.Current.Delegate = new Notifications.iOSNotificationReceiver();

            LoadApplication(new App());

            noties.Init(app);

            return base.FinishedLaunching(app, options);
        }
        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            base.FailedToRegisterForRemoteNotifications(application, error);
        }


        public override void RegisteredForRemoteNotifications(UIApplication application, NSData token)
        {
            noties.Register(token);
        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            noties.Fire();
        }
    }
}
