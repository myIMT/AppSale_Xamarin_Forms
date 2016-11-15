using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using static AppSale.App;
using Android.Webkit;
using Android.Graphics.Drawables;
using Gcm.Client;

namespace AppSale.Droid
{
	[Activity (Label = "AppSale",
		Icon = "@drawable/icon",
		MainLauncher = true,
        LaunchMode = LaunchMode.SingleInstance,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        WindowSoftInputMode = SoftInput.AdjustPan,
		Theme = "@android:style/Theme.Holo.Light")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, IAuthenticate
    {                
        // Define a authenticated user.
        private MobileServiceUser user;

        // Create a new instance field for this activity.
        static MainActivity instance = null;

        // Return the current activity instance.
        public static MainActivity CurrentActivity
        {
            get
            {
                return instance;
            }
        }

        protected override void OnCreate (Bundle bundle)
		{
            //// Set the current instance of MainActivity.
            //instance = this;

            base.OnCreate (bundle);

            ActionBar.SetIcon(new ColorDrawable(Resources.GetColor(Android.Resource.Color.Transparent)));

            // Initialize Azure Mobile Apps
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

			// Initialize Xamarin Forms
			global::Xamarin.Forms.Forms.Init (this, bundle);

            // Initialize the authenticator before loading the app.
            App.Init((IAuthenticate)this);
            // Load the main application
            LoadApplication (new App ());

            //try
            //{
            //    // Check to ensure everything's setup right
            //    GcmClient.CheckDevice(this);
            //    GcmClient.CheckManifest(this);

            //    // Register for push notifications
            //    System.Diagnostics.Debug.WriteLine("Registering...");
            //    GcmClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);
            //}
            //catch (Java.Net.MalformedURLException)
            //{
            //    CreateAndShowDialog("There was an error creating the client. Verify the URL.", "Error");
            //}
            //catch (Exception e)
            //{
            //    CreateAndShowDialog(e.Message, "Error");
            //}
        }



        public async Task<bool> AuthenticateAsync()
        {
            bool success = false;
            try
            {
                if (user == null)
                {
                    // The authentication provider could also be Facebook, Twitter, or Microsoft
                    user = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook);
                    if (user != null)
                    {
                        CreateAndShowDialog("You are now logged in", "Logged in!");
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex.Message, "Authentication failed");
            }
            return success;
        }

        public async Task<bool> LogoutAsync()
        {
            bool success = false;
            try
            {
                if (user != null)
                {
                    CookieManager.Instance.RemoveAllCookie();
                    await TodoItemManager.DefaultManager.CurrentClient.LogoutAsync();
                    CreateAndShowDialog(string.Format("You are now logged out - {0}", user.UserId), "Logged out!");
                }
                user = null;
                success = true;
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex.Message, "Logout failed");
            }

            return success;
        }

        void CreateAndShowDialog(string message, string title)
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.SetNeutralButton("OK", (sender, args) =>
            {
            });
            builder.Create().Show();
        }

        //private void CreateAndShowDialog(String message, String title)
        //{
        //    AlertDialog.Builder builder = new AlertDialog.Builder(this);

        //    builder.SetMessage(message);
        //    builder.SetTitle(title);
        //    builder.Create().Show();
        //}
    }
}

