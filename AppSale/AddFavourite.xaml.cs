using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppSale
{
    public partial class AddFavourite : ContentPage
    {
        TodoItemManager manager;
        Favourites favourites;

        public AddFavourite()
        {
            InitializeComponent();

            manager = TodoItemManager.DefaultManager;

            //// OnPlatform<T> doesn't currently support the "Windows" target platform, so we have this check here.
            //if (manager.IsOfflineEnabled &&
            //    (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone))
            //{
            //    var syncButton = new Button
            //    {
            //        Text = "Sync items",
            //        HeightRequest = 30
            //    };
            //    syncButton.Clicked += OnSyncItems;

            //    buttonsPanel.Children.Add(syncButton);
            //}
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();

        //    Type type = typeof(Favourites); // Get type pointer
        //    //await DisplayAlert("Alert", type.Name, "OK");
        //    //FieldInfo[] fields = type.; // Obtain all fields
        //    //foreach (TypeInfo properties in typeof(Favourites).GetTypeInfo())
        //    //{
        //    //    await DisplayAlert("Alert", properties.Name, "OK");
        //    //    //Console.WriteLine("{0} = {1}", prop.Name, prop.GetValue(user, null));
        //    //}

        //    // Set syncItems to true in order to synchronize the data on startup when running in offline mode
        //    await RefreshItems(true, syncItems: false);
        //}
        public async void AddClick(object sender, EventArgs e)
        {
            Favourites favourites = new Favourites();

            favourites.FashionAndBeauty = Convert.ToInt32(text1Entry.Text);
            favourites.Pets = Convert.ToInt32(text2Entry.Text);
            favourites.SportsAndOutdoor = Convert.ToInt32(text1Entry.Text);
            //await DisplayAlert("Alert", "This screen only shows once, afterwards these settings can be changed via Settings menu", "OK");
            //var todo = new TodoItem { Name = newItemName.Text };
            //await AddItem(todo);

            //newItemName.Text = string.Empty;
            //newItemName.Unfocus();
            await AddItem(favourites);
        }

        public async Task AddItem(Favourites item)
        {
            await manager.SaveTaskAsync(item);
            //todoList.ItemsSource = await manager.GetTodoItemsAsync();
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
