using Microsoft.WindowsAzure.MobileServices;
using Multiselect;
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
    public partial class GetFavourites : ContentPage
    {
        TodoItemManager manager;
        Favourites favourites;
        Favourites favourite = new Favourites();
        IMobileServiceTable<Favourites> favouritesTable;
        SelectMultipleBasePage<CheckItem> multiPage;

        public GetFavourites()
        {
            InitializeComponent();

            manager = TodoItemManager.DefaultManager;
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();

        //    // Refresh items only when authenticated.
        //    if (authenticated == true)
        //    {
        //        // Set syncItems to true in order to synchronize the data 
        //        // on startup when running in offline mode.
        //        await RefreshItems(true, syncItems: false);

        //        // Hide the Sign-in button.
        //        this.loginButton.IsVisible = false;
        //    }
        //}
        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return; // has been set to null, do not 'process' tapped event
            DisplayAlert("Item", "Tapped: " + e.Item, "OK");
            //Debug.WriteLine("Tapped: " + e.Item);
            ((ListView)sender).SelectedItem = null; // de-select the row
        }

        public async void GetClick(object sender, EventArgs e)
        {
            Favourites favourites = new Favourites();
            await GetItem();
        }

        async Task GetItem()
        {
            //Favourites favourite = new Favourites();
            //ListView myListView = new ListView();

            favourite = await manager.RecordLookup(Settings.UserId);

            //IEnumerable<Favourites> items = await favouritesTable
            //                    .Where(favouritesItem => favouritesItem.UserId == Settings.UserId)
            //    .ToEnumerableAsync();
            ////IEnumerable<Favourites> items = await favouritesTable
            ////    .Where(favouritesItem.item1> 3)
            ////    .ToEnumerableAsync();

            //return new ObservableCollection<Favourites>(items);
        }

        public async void OnComplete(object sender, EventArgs e)
        {
            await DisplayAlert("Item", "Nothing", "Got it");
            //var mi = ((MenuItem)sender);
            //var todo = mi.CommandParameter as Favourites;
            //await CompleteItem(todo);
        }

        public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var todo = e.SelectedItem as Favourites;
            await DisplayAlert("Item", "Item selected:  " + todo.FashionAndBeauty, "Got it");
            //if (Device.OS != TargetPlatform.iOS && todo != null)
            //{
            //    // Not iOS - the swipe-to-delete is discoverable there
            //    if (Device.OS == TargetPlatform.Android)
            //    {
            //        await DisplayAlert(todo.Name, "Press-and-hold to complete task " + todo.FashionAndBeauty, "Got it!");
            //    }
            //    else
            //    {
            //        // Windows, not all platforms support the Context Actions yet
            //        if (await DisplayAlert("Mark completed?", "Do you wish to complete " + todo.Name + "?", "Complete", "Cancel"))
            //        {
            //            await CompleteItem(todo);
            //        }
            //    }
            //}

            // prevents background getting highlighted
            //todoList.SelectedItem = null;
        }

        async Task CompleteItem(Favourites item)
        {
            await DisplayAlert("Item", "More nothingness", "Got it");
            //item.Done = true;
            //await manager.SaveTaskAsync(item);
            //todoList.ItemsSource = await manager.GetTodoItemsAsync();
        }

        public async void OnRefresh(object sender, EventArgs e)
        {
            //var list = (ListView)sender;
            //Exception error = null;
            //try
            //{
            //    await RefreshItems(false, true);
            //}
            //catch (Exception ex)
            //{
            //    error = ex;
            //}
            //finally
            //{
            //    list.EndRefresh();
            //}

            //if (error != null)
            //{
            await DisplayAlert("Alert", "Refresh E!!", "OK");
            //}
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

        private class ActivityIndicatorScope : IDisposable
        {
            private bool showIndicator;
            private ActivityIndicator indicator;
            private Task indicatorDelay;

            public ActivityIndicatorScope(ActivityIndicator indicator, bool showIndicator)
            {
                this.indicator = indicator;
                this.showIndicator = showIndicator;

                if (showIndicator)
                {
                    indicatorDelay = Task.Delay(2000);
                    SetIndicatorActivity(true);
                }
                else
                {
                    indicatorDelay = Task.FromResult(0);
                }
            }

            private void SetIndicatorActivity(bool isActive)
            {
                this.indicator.IsVisible = isActive;
                this.indicator.IsRunning = isActive;
            }

            public void Dispose()
            {
                if (showIndicator)
                {
                    indicatorDelay.ContinueWith(t => SetIndicatorActivity(false), TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
        }

        public async void MyFavouritesClick(object sender, EventArgs e)
        {
            GetAllTableRecordsByNoneKey("Favourites", "UserId", Settings.UserId);
            //await GetItem();

            //Type type = typeof(Favourites);                     // Get instance of Favourite class - in order to get to its properties ("Pets", "UserId", "Vehicles", etc.)
            //IEnumerable props = type.GetRuntimeProperties();    // Create a list of Favourite's properties
            //foreach (PropertyInfo prop in props)                // Loop through each property
            //{
            //    if (prop.GetValue(favourite, null) is int)           // Look for all properties that are of type int - The actual "favourite items"
            //    {
            //        if ((int)prop.GetValue(favourite, null) == 1)    // Keep current set "favourite items" and just add newly set "favourite items"
            //        {
            //            await DisplayAlert("prop.Name = ", prop.Name, "OK");

            //            //newValue = (int)tempItem.ElementAt<Favourites>(0).GetType().GetRuntimeProperty(prop.Name).GetValue(tempItem.ElementAt<Favourites>(0), null);
            //            //tempItem.ElementAt<Favourites>(0).GetType().GetRuntimeProperty(prop.Name).SetValue(tempItem.ElementAt<Favourites>(0), (int)prop.GetValue(item, null));
            //        }
            //    }
            //}
        }

        public async void GetTableRecord(string tableName, string key)
        {
            await GetItem();
        }

        public async void GetTableRecordBySql(string key)
        {
            await GetItem();
        }

        public async void GetAllTableRecords(string tableName)
        {
            await GetItem();
        }

        private void SetFavourites(Favourites RetrievedFavourites)
        {
            // Loop through each property to find selectColumn
            Type type = typeof(Favourites);                     // Get instance of Favourite class - in order to get to its properties ("Pets", "UserId", "Vehicles", etc.)
            IEnumerable props = type.GetRuntimeProperties();    // Create a list of Favourite's properties
            foreach (PropertyInfo prop in props)                // Loop through each property
            {
                if (prop.GetValue(RetrievedFavourites, null) is int)           // Look for all properties that are of type int - The actual "favourite items"
                {
                    switch (prop.Name)
                    {
                        case "FashionAndBeauty":
                            favourites.FashionAndBeauty = RetrievedFavourites.FashionAndBeauty;
                            //foreach (var wi in WrappedItems)
                            //{
                            //    wi.IsSelected = true;
                            //}
                            //DisplayAlert("SetFavouriteValue: " + name, favourites.FashionAndBeauty.ToString(), "OK");
                            break;
                        case "SportsAndOutdoor":
                            favourites.SportsAndOutdoor = RetrievedFavourites.SportsAndOutdoor;
                            //DisplayAlert("SetFavouriteValue: " + name, favourites.SportsAndOutdoor.ToString(), "OK");
                            break;
                        case "Pets":
                            favourites.Pets = RetrievedFavourites.Pets;
                            //DisplayAlert("SetFavouriteValue: " + name, favourites.Pets.ToString(), "OK");
                            break;
                        case "Vehicles":
                            favourites.Vehicles = RetrievedFavourites.Vehicles;
                            //DisplayAlert("SetFavouriteValue: " + name, favourites.Vehicles.ToString(), "OK");
                            break;
                        case "HomeImprovement":
                            favourites.HomeImprovement = RetrievedFavourites.HomeImprovement;
                            //DisplayAlert("SetFavouriteValue: " + name, favourites.HomeImprovement.ToString(), "OK");
                            break;
                        case "BabiesChildren":
                            favourites.BabiesChildren = RetrievedFavourites.BabiesChildren;
                            //DisplayAlert("SetFavouriteValue: " + name, favourites.FashionAndBeauty.ToString(), "OK");
                            break;
                        case "HobbiesInterests":
                            favourites.HobbiesInterests = RetrievedFavourites.HobbiesInterests;
                            break;
                        case "MobilePhonesAndAccessories":
                            favourites.MobilePhonesAndAccessories = RetrievedFavourites.MobilePhonesAndAccessories;
                            break;
                        case "HomeAppliances":
                            favourites.HomeAppliances = RetrievedFavourites.HomeAppliances;
                            break;
                        case "Gaming":
                            favourites.Gaming = RetrievedFavourites.Gaming;
                            break;
                        case "Books":
                            favourites.Books = RetrievedFavourites.Books;
                            break;
                        case "Music":
                            favourites.Music = RetrievedFavourites.Music;
                            break;
                        default:
                            //DisplayAlert("NOTHING -- SetFavouriteValue: ", name, "OK");
                            //favourites.[name] = 0;
                            break;
                    }
                }
            }

            //#region FavouritesAndRegionCaptured

            //    #region setRetrievedFavourites
            //    //var favouriteAnswers = multiPage.SetValue()
            //    var favouriteAnswers = multiPage.GetSelection();
            //    foreach (var a in favouriteAnswers)
            //    {
            //        messageLabel.Text += a.Name + ", ";
            //        //ADD CODE HERE - set integer values = 1 for a.Name = Favourites Class
            //        SetFavouriteValue(a.Name);

            //    }
            //    #endregion

            //    #region setSelectedRegions
            //    var ranswers = regionMultiPage.GetSelection();
            //    foreach (var a in ranswers)
            //    {
            //        messageLabel.Text += a.Name + ", ";
            //        //ADD CODE HERE - set integer values = 1 for a.Name = Favourites Class
            //        SetRegionValue(a.Name);

            //    }
            //    #endregion

            //    await Navigation.PushAsync(new GetFavourites());
            //    //await Navigation.PushAsync(new Sale());
            //    await AddRegions(regions);
            //    await AddFavourite(favourites);
            //    //await Navigation.PushAsync(new Sale());
            //#endregion
        }

        public async void GetAllTableRecordsByNoneKey(string tableName, string selectColumn, string key)
        {
            ObservableCollection<Favourites> tempItem = await manager.UserExistAsync();
            //Favourites tempItem = await manager.RecordExistAsync();
            SetFavourites(tempItem.ElementAt<Favourites>(0));
            //// Loop through each property to find selectColumn
            //Type type = typeof(Favourites);                     // Get instance of Favourite class - in order to get to its properties ("Pets", "UserId", "Vehicles", etc.)
            //IEnumerable props = type.GetRuntimeProperties();    // Create a list of Favourite's properties
            //foreach (PropertyInfo prop in props)                // Loop through each property
            //{
            //    if (prop.Name == selectColumn)
            //    {
            //        await DisplayAlert("Selected Column = ", prop.Name, "OK");
            //        string tempName = tempItem.ElementAt<Favourites>(0).GetType().GetRuntimeProperty(prop.Name).Name;


            //    }
            //    //if (prop.GetValue(item, null) is int)           // Look for all properties that are of type int - The actual "favourite items"
            //    //{

            //    //}
            //}



            //        IEnumerable<Favourites> items = await favouritesTable
            //  .Where(favouritesItem => !todoItem.Done)
            //  .ToEnumerableAsync();

            //return new ObservableCollection<TodoItem>(items);
        }
    }
}
