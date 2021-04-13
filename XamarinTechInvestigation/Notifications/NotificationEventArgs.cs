using System;
namespace XamarinTechInvestigation.Notifications
{
    public class NotificationEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
