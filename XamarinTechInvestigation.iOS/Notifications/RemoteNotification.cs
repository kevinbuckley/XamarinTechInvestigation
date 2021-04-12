using System;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.SimpleNotificationService;
using Foundation;
using UIKit;

namespace XamarinTechInvestigation.iOS.Notifications
{
    public class RemoteNotification
    {
        AmazonSimpleNotificationServiceClient snsClient = null;

        public void Init(UIApplication app)
        {
            ConfigureSettings(app);
            ConfigureLogging();
            
            var credentials = new CognitoAWSCredentials(
                "", // obfuscated for public git, it would normally be your AWS Identity pool ID
                RegionEndpoint.USWest2 
            );
            snsClient = new AmazonSimpleNotificationServiceClient(credentials);
        }

        private void ConfigureSettings(UIApplication app)
        {
            AWSConfigs.AWSRegion = "us-west-2"; 
            AWSConfigs.CorrectForClockSkew = true;

            var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                UIUserNotificationType.Alert |
                UIUserNotificationType.Badge |
                UIUserNotificationType.Sound,
                null
            );
            app.RegisterUserNotificationSettings(pushSettings);
            app.RegisterForRemoteNotifications();
        }


        private void ConfigureLogging()
        {
            var loggingConfig = AWSConfigs.LoggingConfig;
            loggingConfig.LogMetrics = true;
            loggingConfig.LogResponses = ResponseLoggingOption.Always;
            loggingConfig.LogMetricsFormat = LogMetricsFormatOption.JSON;
            loggingConfig.LogTo = LoggingOptions.SystemDiagnostics;
        }



        public void Fire()
        {
            var notification = new UILocalNotification();

            // set the fire date (the date time in which it will fire)
            notification.FireDate = NSDate.FromTimeIntervalSinceNow(60);

            // configure the alert
            notification.AlertAction = "Sick alert action";
            notification.AlertBody = "Check out this dope notification?";

            // modify the badge
            notification.ApplicationIconBadgeNumber = 1;

            // set the sound to be the default sound
            notification.SoundName = UILocalNotification.DefaultSoundName;

            // schedule it
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
        }

        public void Register(NSData token)
        {
            var bytes = token.ToArray();
            var deviceToken = BitConverter.ToString(bytes).Replace("-", "");

            try
            {
                if (!string.IsNullOrEmpty(deviceToken))
                {
                    //register with SNS to create an endpoint ARN

                    var response = snsClient.CreatePlatformEndpointAsync(
                        new Amazon.SimpleNotificationService.Model.CreatePlatformEndpointRequest()
                        {
                            
                            Token = deviceToken,
                            PlatformApplicationArn = ""
                            // obfuscated for public git, it would normally be your Platform Arn in the Amazon SNS Application you setup
                        }).Result;
                }
            }
            catch(Exception e)
            {
                string s = e.Message;
                throw;
            }
        }


    }
}
