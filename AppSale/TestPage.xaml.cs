using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppSale
{
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();

        //    // Set syncItems to true in order to synchronize the data on startup when running in offline mode
        //    await RefreshItems(true, syncItems: false);
        //}

        async void OnCreateButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddFavourite());
        }

        async void OnReadButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GetFavourites());
        }

        async void OnUpdateButtonClicked(object sender, EventArgs e)
        {
            //await DisplayAlert("UNDER CONSTRUCTION", "Update existing data in database (cloud-backend)", "OK");
            await Navigation.PushAsync(new UpdateFavourite());
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new GetFavourites());
            await DisplayAlert("UNDER CONSTRUCTION", "Delete existing data in database (cloud-backend)", "OK");
        }
    }
}
