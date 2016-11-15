using Multiselect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppSale
{
    public partial class Welcome : ContentPage
    {
        bool authenticated = false;
        // Track whether the user has authenticated. 
        SelectMultipleBasePage<CheckItem> multiPage;

        public Welcome()
        {
            InitializeComponent();
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ToolbarItems.Clear();
            // Refresh items only when authenticated.
            if (authenticated == true)
            {
                // Set syncItems to true in order to synchronize the data 
                // on startup when running in offline mode.
                //await RefreshItems(true, syncItems: false);
                
                // Hide the Sign-in button.
                this.loginButton.IsVisible = false;
                this.facebookLoginButton.IsVisible = false;
                this.registerButton.IsVisible = false;
                this.forgotButton.IsVisible = false;
                this.logoutButton.IsVisible = true;

                ToolbarItems.Add(new ToolbarItem("Next", "filter.png", async () =>
                {
                    await Navigation.PushAsync(new TestPage());
                }));
            }
            else
            {
                this.loginButton.IsVisible = true;
                this.facebookLoginButton.IsVisible = true;
                this.registerButton.IsVisible = true;
                this.forgotButton.IsVisible = true;
                this.logoutButton.IsVisible = false;
            }

            if (multiPage != null)
            {
                messageLabel.Text = "";
                var answers = multiPage.GetSelection();
                foreach (var a in answers)
                {
                    messageLabel.Text += a.Name + ", ";
                    //ADD CODE HERE - set integer values = 1 for a.Name = Favourites Class
                }
                //Favourites FavItem = SetFavItem(answers);
                //                AddItem();
            }
            else
            {
                messageLabel.Text = "";
            }

        }

        //SelectMultipleBasePage<CheckItem> multiPage;
        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            //DELETE THIS------------------------------
            AppSale.Helpers.Settings.InitFavSet = true;
            //-----------------------------------------
            if (AppSale.Helpers.Settings.InitFavSet)
            {
                //await Navigation.PushAsync(new GetFavourites());
                //await Navigation.PushAsync(new AddFavourite());
                //await Navigation.PushAsync(new TodoList());
                //await Navigation.PushModalAsync(multiPage);
                //-----------------------------------------------------------------------------------------------
                var items = new List<CheckItem>();
                items.Add(new CheckItem { Name = "FASHION & BEAUTY" });
                items.Add(new CheckItem { Name = "SPORTS & OUTDOOR" });
                items.Add(new CheckItem { Name = "PETS" });
                items.Add(new CheckItem { Name = "VEHICLES" });
                items.Add(new CheckItem { Name = "HOME IMPROVEMENT" });
                items.Add(new CheckItem { Name = "BABIES / CHILDREN" });
                items.Add(new CheckItem { Name = "HOOBIES INTERESTS" });
                items.Add(new CheckItem { Name = "MOBILE PHONES & ACCESSORIES" });
                items.Add(new CheckItem { Name = "HOME APPLIANCES" });
                items.Add(new CheckItem { Name = "GAMING" });
                items.Add(new CheckItem { Name = "BOOKS" });
                items.Add(new CheckItem { Name = "MUSIC" });


                //todoList.ItemsSource = items;
                if (multiPage == null)
                    multiPage = new SelectMultipleBasePage<CheckItem>(items) { Title = "Select your favourites" };

                //await Navigation.PushModalAsync(multiPage);
                await Navigation.PushAsync(multiPage);
                //----------------------------------------------------------------------------------------------------
                AppSale.Helpers.Settings.InitFavSet = false;
            }
            else
            {
                await Navigation.PushModalAsync(new Sale());
            }
        }

        public void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            DisplayAlert("Alert", "Screen that handles registration", "OK");
        }

        public void OnForgotButtonClicked(object sender, EventArgs e)
        {
            DisplayAlert("Alert","Screen that handles password and-or username login details retrieval", "OK");
        }

        async void OnFacebookLoginButtonClicked(object sender, EventArgs e)
        {
            try
            {

                if (App.Authenticator != null)
                {
                    authenticated = await App.Authenticator.AuthenticateAsync();
                }

                if (authenticated)
                {
                    //DELETE THIS
                    AppSale.Helpers.Settings.InitFavSet = true;
                    if (AppSale.Helpers.Settings.InitFavSet)
                    {
                        await Navigation.PushAsync(new TestPage());
                        AppSale.Helpers.Settings.InitFavSet = false;
                    }
                    else
                    {
                        await Navigation.PushModalAsync(new Sale());
                    }
                    //Navigation.InsertPageBefore(new TodoList(), this);
                    //await Navigation.PopAsync();
                }
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("Authentication was cancelled"))
                {
                    messageLabel.Text = "Authentication cancelled by the user";
                }
            }
            catch (Exception)
            {
                messageLabel.Text = "Authentication failed";
            }
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            bool loggedOut = false;

            if (App.Authenticator != null)
            {
                loggedOut = await App.Authenticator.LogoutAsync();
            }

            if (loggedOut)
            {
                Navigation.InsertPageBefore(new Welcome(), this);
                await Navigation.PopAsync();
            }
        }
    }
}