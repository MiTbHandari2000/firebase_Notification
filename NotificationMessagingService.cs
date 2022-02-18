using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Firebase.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public delegate void NotificationReceived(bool isclearNotificationFromBanner);
namespace firebase_Notification
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class NotificationMessagingService : FirebaseMessagingService
    {
        private const string TAG = "NotificationMessagingService";
        private string _body;
        public readonly string ChannelId = "my_notification_channel";

        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);
            Log.Debug(TAG, "onNewToken: " + token);
        }
        public override void OnMessageReceived(RemoteMessage message)
        {
            try
            {

                Log.Debug(TAG, "From:" + message.From);


                var notification = message.GetNotification();
                if (notification != null && notification.Body != null)
                {
                    _body = notification.Body;
                }
                else
                {
                    _body = "Push Notification";
                }
                DisplayNotification(notification.Title, _body, message.Data);
            }
            catch (Exception ex)
            {
                Log.Info(TAG, ex.StackTrace);
            }
        }
        void DisplayNotification(string title, string messageBody, IDictionary<string, string> data)
        {
            try
            {
                var intent = new Intent(this, typeof(NotificationActivity));
                var pendingIntent = PendingIntent.GetActivity(this,
                                                              101,
                                                              intent,
                                               PendingIntentFlags.UpdateCurrent);
                var notificationManager = NotificationManagerCompat.From(this);
                if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.O)
                {

                    NotificationChannel notificationChannel =
                        new NotificationChannel(ChannelId, Resources.GetString(Resource.String.app_name), NotificationImportance.High);
                    notificationManager.CreateNotificationChannel(notificationChannel);
                }
                var notificationBuilder = new NotificationCompat.Builder(this, ChannelId)
                                            .SetSmallIcon(Resource.Drawable.saitama)
                                            .SetColor(ContextCompat.GetColor(ApplicationContext, Resource.Color.colorAccent))
                                            .SetColorized(true)
                                            .SetContentTitle(title)
                                            .SetContentText(messageBody)
                                            .SetAutoCancel(true)
                                            .SetDefaults((int)NotificationDefaults.All)
                                           .SetContentIntent(pendingIntent);
                notificationManager.Notify(101, notificationBuilder.Build());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}



