using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTechInvestigation.Notifications;

namespace XamarinTechInvestigation
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Get<INotificationManager>().Initialize();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
