using BumbAlexandruFlaviuLab7.Models;
using Microsoft.Maui.Controls;
using Plugin.LocalNotification;

namespace BumbAlexandruFlaviuLab7;

public partial class ShopPage : ContentPage
{
    public ShopPage()
    {
        InitializeComponent();
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        await App.Database.SaveShopAsync(shop);
        await Navigation.PopAsync();
    }

    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        var address = shop.Adress;


        var locations = await Geocoding.GetLocationsAsync(address);
        var shopLocation = locations?.FirstOrDefault();

        var options = new MapLaunchOptions
        {
            Name = "Magazinul meu preferat"
        };

       
        var myLocation = new Location(46.7731796289, 23.6213886738);

        if (shopLocation != null)
        {
            var distance = myLocation.CalculateDistance(shopLocation, DistanceUnits.Kilometers);

            if (distance < 5)
            {
                var request = new NotificationRequest
                {
                    Title = "Ai de facut cumparaturi in apropiere!",
                    Description = address,
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(1)
                    }
                };

                LocalNotificationCenter.Current.Show(request);
            }

           
            await Map.OpenAsync(shopLocation, options);
        }

    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        if (shop == null)
            return;

        bool answer = await DisplayAlert(
            "Confirm Delete",
            $"Are you sure you want to delete {shop.ShopName}?",
            "Yes", "No");

        if (!answer)
            return;


        await App.Database.DeleteShopAsync(shop);


        await Navigation.PopAsync();
    }

}


