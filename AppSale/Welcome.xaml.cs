using Multiselect;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
                #region FavouritesAndRegionCaptured
                if (setFavourites && setRegions)
                {
                    messageLabel.Text = "";

                    #region setSelectedFavourites
                    var favouriteAnswers = multiPage.GetSelection();
                    foreach (var a in favouriteAnswers)
                    {
                        messageLabel.Text += a.Name + ", ";
                        //ADD CODE HERE - set integer values = 1 for a.Name = Favourites Class
                        SetFavouriteValue(a.Name);

                    } 
                    #endregion

                    #region setSelectedRegions
                    var ranswers = regionMultiPage.GetSelection();
                    foreach (var a in ranswers)
                    {
                        messageLabel.Text += a.Name + ", ";
                        //ADD CODE HERE - set integer values = 1 for a.Name = Favourites Class
                        SetRegionValue(a.Name);

                    }
                    #endregion

                    await Navigation.PushAsync(new GetFavourites());
                    //await Navigation.PushAsync(new Sale());
                    await AddRegions(regions);
                    await AddFavourite(favourites);
                    //await Navigation.PushAsync(new Sale());
                }
                #endregion

                #region FavouritesCapturedNotRegion
                if (setFavourites && (setRegions == false))
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

                    await Navigation.PushAsync(regionMultiPage);
                    //await Navigation.PushAsync(regionMultiPage);
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
                #endregion

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

            //#region FavouriteList
            //if (multiPage != null)
            //{
            //    messageLabel.Text = "";
            //    var favouriteAnswers = multiPage.GetSelection();
            //    foreach (var a in favouriteAnswers)
            //    {
            //        messageLabel.Text += a.Name + ", ";
            //        //ADD CODE HERE - set integer values = 1 for a.Name = Favourites Class
            //        SetFavouriteValue(a.Name);

            //    }
            //    await Navigation.PushModalAsync(new Sale());
            //    await AddRegions(regions);
            //    await AddFavourite(favourites);
            //    //await Navigation.PopAsync(new Welcome());
                
            //}
            //else
            //{
            //    messageLabel.Text = "";
            //}
            //#endregion

            //#region RegionList
            //if (regionMultiPage != null)
            //{
            //    messageLabel.Text = "";
            //    var regionAnswers = regionMultiPage.GetSelection();
            //    foreach (var a in regionAnswers)
            //    {
            //        messageLabel.Text += a.Name + ", ";
            //        //ADD CODE HERE - set integer values = 1 for a.Name = Favourites Class
            //        SetRegionValue(a.Name);

            //    }
            //    //await AddRegions(regions);

            //    //--------------------------------------------------------------------------
            //    //#region DoFavouriteList
            //    //var favouriteItems = new List<CheckItem>();
            //    //favouriteItems.Add(new CheckItem { Name = "FASHION & BEAUTY" });
            //    //favouriteItems.Add(new CheckItem { Name = "SPORTS & OUTDOOR" });
            //    //favouriteItems.Add(new CheckItem { Name = "PETS" });
            //    //favouriteItems.Add(new CheckItem { Name = "VEHICLES" });
            //    //favouriteItems.Add(new CheckItem { Name = "HOME IMPROVEMENT" });
            //    //favouriteItems.Add(new CheckItem { Name = "BABIES / CHILDREN" });
            //    //favouriteItems.Add(new CheckItem { Name = "HOOBIES INTERESTS" });
            //    //favouriteItems.Add(new CheckItem { Name = "MOBILE PHONES & ACCESSORIES" });
            //    //favouriteItems.Add(new CheckItem { Name = "HOME APPLIANCES" });
            //    //favouriteItems.Add(new CheckItem { Name = "GAMING" });
            //    //favouriteItems.Add(new CheckItem { Name = "BOOKS" });
            //    //favouriteItems.Add(new CheckItem { Name = "MUSIC" });


            //    ////todoList.ItemsSource = items;
            //    //if (multiPage == null)
            //    //    multiPage = new SelectMultipleBasePage<CheckItem>(favouriteItems) { Title = "Select your favourites" };

            //    ////await Navigation.PushModalAsync(multiPage);
            //    //await Navigation.PushAsync(multiPage);
            //    //#endregion
            //    //---------------------------------------------------------------------------------
            //}
            //else
            //{
            //    messageLabel.Text = "";
            //} 
            //#endregion
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
            ObservableCollection<Favourites> tempItem = await manager.UserExistAsync();
            //await DisplayAlert("Number of records",tempItem.Count.ToString(),"OK");
            int newValue;

            //if (tempItem != null) || (tempItem != null)
            //{
                if (tempItem.Count == 1)
                {
                    Type type = typeof(Favourites);
                    IEnumerable props = type.GetRuntimeProperties();
                    foreach (PropertyInfo prop in props)
                    {
                        if (prop.GetValue(item, null) is int)
                        {
                            if ((int)prop.GetValue(item, null) == 1)
                            {
                                string tempName = tempItem.ElementAt<Favourites>(0).GetType().GetRuntimeProperty(prop.Name).Name;
                                newValue = (int)tempItem.ElementAt<Favourites>(0).GetType().GetRuntimeProperty(prop.Name).GetValue(tempItem.ElementAt<Favourites>(0), null);
                                tempItem.ElementAt<Favourites>(0).GetType().GetRuntimeProperty(prop.Name).SetValue(tempItem.ElementAt<Favourites>(0), (int)prop.GetValue(item, null));
                                await manager.SaveTaskAsync(tempItem.ElementAt<Favourites>(0));
                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }
                    }
                }
                else
                {//not unique record
                    //Delete duplicate records
                    await manager.SaveFavouriteAsync(item);
                }
            //}
            //else
            //{
            //    await manager.SaveRegionAsync(item);
            //}
        }

        public async Task AddRegions(Regions item)
        {
            ObservableCollection<Regions> tempItem = await manager.RegionUserExistAsync();
            //await DisplayAlert("Number of records", tempItem.Count.ToString(), "OK");
            int newValue;

            //if (tempItem != null)
            //{
                if (tempItem.Count == 1)
                {
                    Type type = typeof(Regions);
                    IEnumerable props = type.GetRuntimeProperties();
                    foreach (PropertyInfo prop in props)
                    {
                        if (prop.GetValue(item, null) is int)
                        {
                            if ((int)prop.GetValue(item, null) == 1)
                            {
                                string tempName = tempItem.ElementAt<Regions>(0).GetType().GetRuntimeProperty(prop.Name).Name;
                                newValue = (int)tempItem.ElementAt<Regions>(0).GetType().GetRuntimeProperty(prop.Name).GetValue(tempItem.ElementAt<Regions>(0), null);
                                tempItem.ElementAt<Regions>(0).GetType().GetRuntimeProperty(prop.Name).SetValue(tempItem.ElementAt<Regions>(0), (int)prop.GetValue(item, null));
                                await manager.SaveRegionAsync(tempItem.ElementAt<Regions>(0));
                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }
                    }
                }
                else
                {//not unique record
                    //
                    await manager.SaveRegionAsync(item);
                }
            //}
            //else
            //{
            //    await manager.SaveRegionAsync(item);
            //}
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
                var regionsItems = new List<CheckItem>();
                regionsItems.Add(new CheckItem { Name = "Eastern Cape" });
                regionsItems.Add(new CheckItem { Name = "Free State" });
                regionsItems.Add(new CheckItem { Name = "Gauteng" });
                regionsItems.Add(new CheckItem { Name = "KwaZulu-Natal" });
                regionsItems.Add(new CheckItem { Name = "Limpopo" });
                regionsItems.Add(new CheckItem { Name = "Mpumalanga" });
                regionsItems.Add(new CheckItem { Name = "Northern Cape" });
                regionsItems.Add(new CheckItem { Name = "North West" });
                regionsItems.Add(new CheckItem { Name = "Western Cape" });

                //todoList.ItemsSource = items;
                if (regionMultiPage == null)
                    regionMultiPage = new SelectMultipleBasePage<CheckItem>(regionsItems) { Title = "Select your region" };

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

                //-----------------------------------------------------------------------------------------------
                //var favouriteItems = new List<CheckItem>();
                //favouriteItems.Add(new CheckItem { Name = "FASHION & BEAUTY" });
                //favouriteItems.Add(new CheckItem { Name = "SPORTS & OUTDOOR" });
                //favouriteItems.Add(new CheckItem { Name = "PETS" });
                //favouriteItems.Add(new CheckItem { Name = "VEHICLES" });
                //favouriteItems.Add(new CheckItem { Name = "HOME IMPROVEMENT" });
                //favouriteItems.Add(new CheckItem { Name = "BABIES / CHILDREN" });
                //favouriteItems.Add(new CheckItem { Name = "HOOBIES INTERESTS" });
                //favouriteItems.Add(new CheckItem { Name = "MOBILE PHONES & ACCESSORIES" });
                //favouriteItems.Add(new CheckItem { Name = "HOME APPLIANCES" });
                //favouriteItems.Add(new CheckItem { Name = "GAMING" });
                //favouriteItems.Add(new CheckItem { Name = "BOOKS" });
                //favouriteItems.Add(new CheckItem { Name = "MUSIC" });


                ////todoList.ItemsSource = items;
                //if (multiPage == null)
                //    multiPage = new SelectMultipleBasePage<CheckItem>(favouriteItems) { Title = "Select your favourites" };

                ////await Navigation.PushModalAsync(multiPage);
                //await Navigation.PushAsync(multiPage);
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
                    //await DisplayAlert("UserID:", Settings.UserId,"OK");
                    //DELETE THIS
                    AppSale.Helpers.Settings.InitFavSet = true;
                    if (AppSale.Helpers.Settings.InitFavSet)
                    {
                        //DELETE THIS------------------------------
                        AppSale.Helpers.Settings.InitFavSet = true;
                        //-----------------------------------------
                        if (AppSale.Helpers.Settings.InitFavSet)
                        {
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


                            if (multiPage == null)
                                multiPage = new SelectMultipleBasePage<CheckItem>(items) { Title = "Select your favourites" };

                            await Navigation.PushAsync(multiPage);
                            AppSale.Helpers.Settings.InitFavSet = false;

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