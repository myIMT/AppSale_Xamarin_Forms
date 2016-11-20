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
        SelectMultipleBasePage<CheckItem> regionMultiPage;
        Favourites favourites = new Favourites();
        TodoItemManager manager;
        Regions regions = new Regions();
        bool setRegions = false;
        bool setFavourites = false;

        public Welcome()
        {
            InitializeComponent();
            manager = TodoItemManager.DefaultManager;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ToolbarItems.Clear();
            // Refresh items only when authenticated.
            if (authenticated == true)
            {
                if (setFavourites && setRegions)
                {
                    var ranswers = regionMultiPage.GetSelection();
                    foreach (var a in ranswers)
                    {
                        messageLabel.Text += a.Name + ", ";
                        //ADD CODE HERE - set integer values = 1 for a.Name = Favourites Class
                        SetRegionValue(a.Name);

                    }
                    await AddRegions(regions);
                    await Navigation.PushAsync(new Sale());
                }

                if (setFavourites && (setRegions==false))
                {
                    var items = new List<CheckItem>();
                    items.Add(new CheckItem { Name = "Eastern Cape" });
                    items.Add(new CheckItem { Name = "Free State" });
                    items.Add(new CheckItem { Name = "Gauteng" });
                    items.Add(new CheckItem { Name = "KwaZulu-Natal" });
                    items.Add(new CheckItem { Name = "Limpopo" });
                    items.Add(new CheckItem { Name = "Mpumalanga" });
                    items.Add(new CheckItem { Name = "Northern Cape" });
                    items.Add(new CheckItem { Name = "North West" });
                    items.Add(new CheckItem { Name = "Western Cape" });

                    //todoList.ItemsSource = items;
                    if (regionMultiPage == null)
                        regionMultiPage = new SelectMultipleBasePage<CheckItem>(items) { Title = "Select your region" };

                    //await Navigation.PushModalAsync(multiPage);
                    await Navigation.PushAsync(regionMultiPage);
                    messageLabel.Text = "";
                    //var ranswers = regionMultiPage.GetSelection();
                    //foreach (var a in ranswers)
                    //{
                    //    messageLabel.Text += a.Name + ", ";
                    //    //ADD CODE HERE - set integer values = 1 for a.Name = Favourites Class
                    //    SetRegionValue(a.Name);

                    //}
                    //await AddRegions(regions);
                    setRegions = true;
                    }
                    else
                    {
                        messageLabel.Text = "";
                    }

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
                    SetFavouriteValue(a.Name);

                }
                await AddFavourite(favourites);
            }
            else
            {
                messageLabel.Text = "";
            }

        }

        private void ZeroFavouriteItems()
        {
            favourites.FashionAndBeauty = 0;
            favourites.SportsAndOutdoor = 0;
            favourites.Pets = 0;
            favourites.Vehicles = 0;
            favourites.HomeImprovement = 0;
            favourites.BabiesChildren = 0;
            favourites.HobbiesInterests = 0;
            favourites.MobilePhonesAndAccessories = 0;
            favourites.HomeAppliances = 0;
            favourites.Gaming = 0;
            favourites.Books = 0;
            favourites.Music = 0;
        }

        private void SetFavouriteValue(string name)
        {
            switch (name)
            {
                case "FASHION & BEAUTY":
                    favourites.FashionAndBeauty = 1;
                    //DisplayAlert("SetFavouriteValue: " + name, favourites.FashionAndBeauty.ToString(), "OK");
                    break;
                case "SPORTS & OUTDOOR":
                    favourites.SportsAndOutdoor = 1;
                    //DisplayAlert("SetFavouriteValue: " + name, favourites.SportsAndOutdoor.ToString(), "OK");
                    break;
                case "PETS":
                    favourites.Pets = 1;
                    //DisplayAlert("SetFavouriteValue: " + name, favourites.Pets.ToString(), "OK");
                    break;
                case "VEHICLES":
                    favourites.Vehicles = 1;
                    //DisplayAlert("SetFavouriteValue: " + name, favourites.Vehicles.ToString(), "OK");
                    break;
                case "HOME IMPROVEMENT":
                    favourites.HomeImprovement = 1;
                    //DisplayAlert("SetFavouriteValue: " + name, favourites.HomeImprovement.ToString(), "OK");
                    break;


                case "BABIES/CHILDREN":
                    favourites.BabiesChildren = 1;
                    //DisplayAlert("SetFavouriteValue: " + name, favourites.FashionAndBeauty.ToString(), "OK");
                    break;
                case "HOBBIES/INTERESTS":
                    favourites.HobbiesInterests = 1;
                    break;
                case "MOBILE PHONES & ACCESSORIES":
                    favourites.MobilePhonesAndAccessories = 1;
                    break;
                case "HOME APPLIANCES":
                    favourites.HomeAppliances = 1;
                    break;
                case "GAMING":
                    favourites.Gaming = 1;
                    break;
                case "BOOKS":
                    favourites.Books = 1;
                    break;
                case "MUSIC":
                    favourites.Music = 1;
                    break;
                default:
                    //DisplayAlert("NOTHING -- SetFavouriteValue: ", name, "OK");
                    //favourites.[name] = 0;
                    break;
            }
        }

        private void SetRegionValue(string name)
        {
            switch (name)
            {
                case "Eastern Cape":
                    regions.EasternCape = 1;
                    //DisplayAlert("SetFavouriteValue: " + name, favourites.FashionAndBeauty.ToString(), "OK");
                    break;
                case "Free State":
                    regions.FreeState = 1;
                    //DisplayAlert("SetFavouriteValue: " + name, favourites.SportsAndOutdoor.ToString(), "OK");
                    break;
                case "Gauteng":
                    regions.Gauteng = 1;
                    //DisplayAlert("SetFavouriteValue: " + name, favourites.Pets.ToString(), "OK");
                    break;
                case "KwaZulu-Natal":
                    regions.KwaZuluNatal = 1;
                    //DisplayAlert("SetFavouriteValue: " + name, favourites.Vehicles.ToString(), "OK");
                    break;
                case "Limpopo":
                    regions.Limpopo = 1;
                    //DisplayAlert("SetFavouriteValue: " + name, favourites.HomeImprovement.ToString(), "OK");
                    break;
                case "Mpumalanga":
                    regions.Mpumalanga = 1;
                    //DisplayAlert("SetFavouriteValue: " + name, favourites.FashionAndBeauty.ToString(), "OK");
                    break;
                case "Northern Cape":
                    regions.NorthernCape = 1;
                    break;
                case "North West":
                    regions.NorthWest = 1;
                    break;
                case "Western Cape":
                    regions.WesternCape = 1;
                    break;
                default:
                    //DisplayAlert("NOTHING -- SetFavouriteValue: ", name, "OK");
                    //favourites.[name] = 0;
                    break;
            }
        }

        public async Task AddFavourite(Favourites item)
        {
            await manager.SaveTaskAsync(item);
            //todoList.ItemsSource = await manager.GetTodoItemsAsync();
        }

        public async Task AddRegions(Regions item)
        {
            await manager.SaveRegionAsync(item);
            //todoList.ItemsSource = await manager.GetTodoItemsAsync();
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
            DisplayAlert("Alert", "Screen that handles password and-or username login details retrieval", "OK");
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
                            items.Add(new CheckItem { Name = "BABIES/CHILDREN" });
                            items.Add(new CheckItem { Name = "HOBBIES/INTERESTS" });
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
                            //setRegions = true;
                            setFavourites = true;
                        }
                        else
                        {
                            await Navigation.PushModalAsync(new Sale());
                        }
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