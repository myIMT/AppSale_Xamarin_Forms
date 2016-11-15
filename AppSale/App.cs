using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppSale
{
	public class App : Application
	{
        //public interface IAuthenticate
        //{
        //    Task<bool> Authenticate();
        //}

        public App ()
		{
            // The root page of your application
            //MainPage = new TodoList();
            MainPage = new NavigationPage(new Welcome());
            //MainPage = new Welcome();
        }

        public static IAuthenticate Authenticator { get; private set; }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }

        protected override void OnStart ()
		{
			// Handle when your app startss
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

