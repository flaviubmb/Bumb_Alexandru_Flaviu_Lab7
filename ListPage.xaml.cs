using Microsoft.Maui.Controls;
using BumbAlexandruFlaviuLab7.Models;
using System;

namespace BumbAlexandruFlaviuLab7
{

    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
        }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var slist = (ShopList)BindingContext;
            slist.Date = DateTime.UtcNow;

            await App.Database.SaveShopListAsync(slist);
            await Navigation.PopAsync();
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var slist = (ShopList)BindingContext;
            await App.Database.DeleteShopListAsync(slist);
            await Navigation.PopAsync();
        }
    }


}
