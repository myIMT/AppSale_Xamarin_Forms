//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Xamarin.Forms;
//using System.Threading.Tasks;

//[assembly: Dependency(typeof(Authentication))]
//namespace AppSale.Droid
//{
//    public class Authentication : IAuthentication
//    {
//        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider)
//        {
//            try
//            {
//                //login and save user status
//                var user = await client.LoginAsync(Forms.Context, provider);
//                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
//                Settings.UserId = user?.UserId ?? string.Empty;
//                return user;
//            }
//            catch (Exception e)
//            {
//                e.Data["method"] = "LoginAsync";
//                Xamarin.Insights.Report(e);
//            }

//            return null;
//        }

//    }
//}