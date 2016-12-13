using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppSale
{
    public partial class Sale : ContentPage
    {
        Favourites favourites = new Favourites();
        TodoItemManager manager;
        Regions regions = new Regions();

        public Sale()
        {
            InitializeComponent();
            manager = TodoItemManager.DefaultManager;
        }

        async void SaleButtonClicked(object sender, EventArgs e)
        {
            //DisplayAlert("Alert", "Screen that handles registration", "OK");
            await manager.GetFavouritesAsync();
        }

        public async Task GetFavourites(Regions item)
        {
            ListView myListView = new ListView();
            //favouriteList.ItemsSource = await manager.GetFavouritesAsync();
            //todoList.ItemsSource = await manager.SaveRegionAsync(item);
            //todoList.ItemsSource = await manager.GetTodoItemsAsync();
        }

        //// Event handlers
        //public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var fav = e.SelectedItem as Favourites;
        //    if (Device.OS != TargetPlatform.iOS && fav != null)
        //    {
        //        // Not iOS - the swipe-to-delete is discoverable there
        //        if (Device.OS == TargetPlatform.Android)
        //        {
        //            await DisplayAlert(fav.UserId, "Press-and-hold to complete task " + fav.UserId, "Got it!");
        //        }
        //        else
        //        {
        //            // Windows, not all platforms support the Context Actions yet
        //            if (await DisplayAlert("Mark completed?", "Do you wish to complete " + fav.UserId + "?", "Complete", "Cancel"))
        //            {
        //                await CompleteItem(fav);
        //            }
        //        }
        //    }

        //    // prevents background getting highlighted
        //    favouriteList.SelectedItem = null;
        //}

        //public async void OnComplete(object sender, EventArgs e)
        //{
        //    var mi = ((MenuItem)sender);
        //    var fav = mi.CommandParameter as Favourites;
        //    await CompleteItem(fav);
        //}

        //async Task CompleteItem(Favourites item)
        //{
        //    item.Done = true;
        //    await manager.SaveTaskAsync(item);
        //    favouriteList.ItemsSource = await manager.GetFavouritesAsync();
        //}

        //public async void OnRefresh(object sender, EventArgs e)
        //{
        //    var list = (ListView)sender;
        //    Exception error = null;
        //    try
        //    {
        //        await RefreshItems(false, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        error = ex;
        //    }
        //    finally
        //    {
        //        list.EndRefresh();
        //    }

        //    if (error != null)
        //    {
        //        await DisplayAlert("Refresh Error", "Couldn't refresh data (" + error.Message + ")", "OK");
        //    }
        //}

        //private async Task RefreshItems(bool showActivityIndicator, bool syncItems)
        //{
        //    using (var scope = new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
        //    {
        //        favouriteList.ItemsSource = await manager.GetTodoItemsAsync(syncItems);
        //    }
        //}

        //private class ActivityIndicatorScope : IDisposable
        //{
        //    private bool showIndicator;
        //    private ActivityIndicator indicator;
        //    private Task indicatorDelay;

        //    public ActivityIndicatorScope(ActivityIndicator indicator, bool showIndicator)
        //    {
        //        this.indicator = indicator;
        //        this.showIndicator = showIndicator;

        //        if (showIndicator)
        //        {
        //            indicatorDelay = Task.Delay(2000);
        //            SetIndicatorActivity(true);
        //        }
        //        else
        //        {
        //            indicatorDelay = Task.FromResult(0);
        //        }
        //    }

        //    private void SetIndicatorActivity(bool isActive)
        //    {
        //        this.indicator.IsVisible = isActive;
        //        this.indicator.IsRunning = isActive;
        //    }

        //    public void Dispose()
        //    {
        //        if (showIndicator)
        //        {
        //            indicatorDelay.ContinueWith(t => SetIndicatorActivity(false), TaskScheduler.FromCurrentSynchronizationContext());
        //        }
        //    }
        //}
    }
}
