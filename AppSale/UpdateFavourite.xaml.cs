using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppSale
{
    public partial class UpdateFavourite : ContentPage
    {
        TodoItemManager manager;
        Favourites favourites;

        public UpdateFavourite()
        {
            InitializeComponent();

            manager = TodoItemManager.DefaultManager;
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return; // has been set to null, do not 'process' tapped event
            //DisplayAlert("Item", "Tapped: " + e.Item, "OK");
            //Debug.WriteLine("Tapped: " + e.Item);
            ((ListView)sender).SelectedItem = null; // de-select the row
        }

        public async void UpdateClick(object sender, EventArgs e)
        {
            Favourites favourites = new Favourites();
            await GetItem();
        }

        async Task GetItem()
        {
            Favourites favourite = new Favourites();
            ListView myListView = new ListView();

            //myListView.ItemsSource =  await manager.GetFavouritesAsync();
            todoList.ItemsSource = await manager.GetFavouritesAsync();
            //todoList.ItemsSource = await manager.GetTodoItemsAsync();
            //favourite = myListView
            //todoList.ItemsSource = myListView.ItemsSource;
            //await DisplayAlert("Alert", myListView.ItemsSource.ToString(), "OK");
            todoList.ItemTapped += TodoList_ItemTapped;
            UpdateBtn.Text = "Update";
        }

        private void TodoList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return; // has been set to null, do not 'process' tapped event
            //DisplayAlert("Item", "Tapped: " + e.Item, "OK");
            //Debug.WriteLine("Tapped: " + e.Item);
            //((ListView)sender).SelectedItem = null; // de-select the row
            favourites = (Favourites)e.Item;
            text1Entry.Text = favourites.FashionAndBeauty.ToString();
            text2Entry.Text = favourites.Pets.ToString();
            text3Entry.Text = favourites.SportsAndOutdoor.ToString();

            text1Entry.IsEnabled = true;
            text2Entry.IsEnabled = true;
            text3Entry.IsEnabled = true;
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
            //await DisplayAlert("Item", "Item selected:  " + todo.FashionAndBeauty, "Got it");
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
    }
}
