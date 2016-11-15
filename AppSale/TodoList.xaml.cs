using Multiselect;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppSale
{
    public partial class TodoList : ContentPage
    {
        TodoItemManager manager;
        Favourites favourites;

        public TodoList()
        {
            InitializeComponent();

            manager = TodoItemManager.DefaultManager;

            // OnPlatform<T> doesn't currently support the "Windows" target platform, so we have this check here.
            if (manager.IsOfflineEnabled &&
                (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone))
            {
                var syncButton = new Button
                {
                    Text = "Sync items",
                    HeightRequest = 30
                };
                syncButton.Clicked += OnSyncItems;

                buttonsPanel.Children.Add(syncButton);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Type type = typeof(Favourites); // Get type pointer
            await DisplayAlert("Alert", type.Name, "OK");
            //FieldInfo[] fields = type.; // Obtain all fields
            //foreach (TypeInfo properties in typeof(Favourites).GetTypeInfo())
            //{
            //    await DisplayAlert("Alert", properties.Name, "OK");
            //    //Console.WriteLine("{0} = {1}", prop.Name, prop.GetValue(user, null));
            //}

            // Set syncItems to true in order to synchronize the data on startup when running in offline mode
            await RefreshItems(true, syncItems: false);

            if (multiPage != null)
            {
                results.Text = "";
                var answers = multiPage.GetSelection();
                foreach (var a in answers)
                {
                    results.Text += a.Name + ", ";
                }
                 //Favourites FavItem = SetFavItem(answers);
//                AddItem();
            }
            else
            {
                results.Text = "(none)";
            }
        }

        private Favourites SetFavItem(List<CheckItem> answers)
        {
            


            //Favourites favourites;
            //DisplayAlert("Alert", favourites.FashionAndBeauty.ToString(), "OK");
            foreach (var a in answers)
            {
                //results.Text += a.Name + ", ";
                //if (a.Name == favourites.)
                //{

                //}
                //else if (true)
                //{

                //}
            }
            return favourites;
        }

        // Data methods
        async Task AddItem(Favourites item)
        {
            await manager.SaveTaskAsync(item);
            //todoList.ItemsSource = await manager.GetTodoItemsAsync();
        }

        async Task CompleteItem(Favourites item)
        {
            item.Done = true;
            await manager.SaveTaskAsync(item);
            //todoList.ItemsSource = await manager.GetTodoItemsAsync();
        }

        public async void OnSubmit(object sender, EventArgs e)
        {
            await DisplayAlert("Alert", "This screen only shows once, afterwards these settings can be changed via Settings menu", "OK");
            //var todo = new TodoItem { Name = newItemName.Text };
            //await AddItem(todo);

            //newItemName.Text = string.Empty;
            //newItemName.Unfocus();
        }

        SelectMultipleBasePage<CheckItem> multiPage;
        public async void OnDisplay(object sender, EventArgs e)
        {
            //await DisplayAlert("Alert", "This screen only shows once, afterwards these settings can be changed via Settings menu", "OK");
            //SelectMultipleBasePage<CheckItem> multiPage;

            var items = new List<CheckItem>();
            items.Add(new CheckItem { Name = "Xamarin.com" });
            items.Add(new CheckItem { Name = "Twitter" });
            items.Add(new CheckItem { Name = "Facebook" });
            items.Add(new CheckItem { Name = "Internet ad" });
            items.Add(new CheckItem { Name = "Online article" });
            items.Add(new CheckItem { Name = "Magazine ad" });
            items.Add(new CheckItem { Name = "Friends" });
            items.Add(new CheckItem { Name = "At work" });


            //todoList.ItemsSource = items;
            if (multiPage == null)
                multiPage = new SelectMultipleBasePage<CheckItem>(items) { Title = "Check all that apply" };

            await Navigation.PushAsync(multiPage);
            //await Navigation.PushAsync(multiPage);
        }

        // Event handlers
        public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //var todo = e.SelectedItem as Favourites;
            //if (Device.OS != TargetPlatform.iOS && todo != null)
            //{
            //    // Not iOS - the swipe-to-delete is discoverable there
            //    if (Device.OS == TargetPlatform.Android)
            //    {
            //        await DisplayAlert(todo.SportsAndOutdoor, "Press-and-hold to complete task " + todo.SportsAndOutdoor, "Got it!");
            //    }
            //    else
            //    {
            //        // Windows, not all platforms support the Context Actions yet
            //        if (await DisplayAlert("Mark completed?", "Do you wish to complete " + todo.FashionAndBeauty + "?", "Complete", "Cancel"))
            //        {
            //            await CompleteItem(todo);
            //        }
            //    }
            //}

            // prevents background getting highlighted
            //todoList.SelectedItem = null;
        }

        // http://developer.xamarin.com/guides/cross-platform/xamarin-forms/working-with/listview/#context
        public async void OnComplete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var todo = mi.CommandParameter as Favourites;
            await CompleteItem(todo);
        }

        // http://developer.xamarin.com/guides/cross-platform/xamarin-forms/working-with/listview/#pulltorefresh
        public async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            Exception error = null;
            try
            {
                await RefreshItems(false, true);
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                list.EndRefresh();
            }

            if (error != null)
            {
                await DisplayAlert("Refresh Error", "Couldn't refresh data (" + error.Message + ")", "OK");
            }
        }

        public async void OnSyncItems(object sender, EventArgs e)
        {
            await RefreshItems(true, true);
        }

        private async Task RefreshItems(bool showActivityIndicator, bool syncItems)
        {
            //ListView myListView = new ListView();
            //myListView.ItemsSource = await manager.GetTodoItemsAsync(syncItems);

            using (var scope = new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
            {
                string myResult = "";
                //-----------------------------------------------------------------------------------------
                //if (multiPage != null)
                //{
                //    var answers = multiPage.GetSelection();
                //    foreach (var a in answers)
                //    {
                //        myResult += a.Name + ", ";
                //    }
                //}
                //else
                //{
                //    myResult = "(none)";
                //}
                //await DisplayAlert("Alert", myResult, "OK");
                //-------------------------------------------------------------------------------------------
                //await DisplayAlert("Alert", "data retrieval happens here", "OK");
                //todoList.ItemsSource = await manager.GetTodoItemsAsync(syncItems);
                //await DisplayAlert("Alert", "the end", "OK");
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

