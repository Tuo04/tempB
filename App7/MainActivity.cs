using Android.App;
using Android.Widget;
using Android.OS;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Azure.Mobile.Push;

namespace App7
{
    [Activity(Label = "App7", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // This should come before MobileCenter.Start() is called
            Push.PushNotificationReceived += (sender, e) => {

                // Add the notification message and title to the message
                var summary = $"Push notification received:" +
                                    $"\n\tNotification title: {e.Title}" +
                                    $"\n\tMessage: {e.Message}";

                // If there is custom data associated with the notification,
                // print the entries
                if (e.CustomData != null)
                {
                    summary += "\n\tCustom data:\n";
                    foreach (var key in e.CustomData.Keys)
                    {
                        summary += $"\t\t{key} : {e.CustomData[key]}\n";
                    }
                }

                // Send the notification summary to debug output
                System.Diagnostics.Debug.WriteLine(summary);
            };

            Button button = FindViewById<Button>(Resource.Id.button1);
            button.Click += delegate { onCrash(); };

            MobileCenter.Start("861d550c-b8f5-4c90-a39e-e367a110ec7b",
                   typeof(Analytics), typeof(Crashes),typeof(Push));

        }

        public void onCrash() {
            Crashes.GenerateTestCrash();
        }
    }
}

