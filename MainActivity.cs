using Android.App;
using Android.Gms.Extensions;
using Android.OS;
using Android.Runtime;
using Android.Util;
using AndroidX.AppCompat.App;
using Firebase.Messaging;
using System;
using System.Threading.Tasks;

namespace firebase_Notification
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]

    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.notification_activity);
            _ = GetFirbaseToken();
        }

        private async Task GetFirbaseToken()
        {
            try
            {
                var token = await FirebaseMessaging.Instance.GetToken();
            }
            catch (Exception ex)
            {
                Log.Debug("klasfjsadjkbjasdbn", ex.Message);
            }
        }
    }
}