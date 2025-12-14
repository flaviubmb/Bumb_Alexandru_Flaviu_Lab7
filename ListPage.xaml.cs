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

            Shop selectedShop = (ShopPicker.SelectedItem as Shop);
            slist.ShopID = selectedShop.ID;

            await App.Database.SaveShopListAsync(slist);
            await Navigation.PopAsync();
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var slist = (ShopList)BindingContext;
            await App.Database.DeleteShopListAsync(slist);
            await Navigation.PopAsync();
        }

        private async void OnDeleteItemButtonClicked(object sender, EventArgs e)
        {
        
            var selectedProduct = listView.SelectedItem as Product;
            if (selectedProduct != null)
            {
           
                await App.Database.DeleteProductAsync(selectedProduct);

               
                var shopl = BindingContext as ShopList;
                if (shopl != null)
                {
                    listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
                }
            }
            else
            {
                await DisplayAlert("Attention",
                     "Please select a product before deleting.",
                     "OK");

            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var items = await App.Database.GetShopsAsync();

            ShopPicker.ItemsSource = (System.Collections.IList)items;
            ShopPicker.ItemDisplayBinding = new Binding("ShopDetails");


            var shopl = BindingContext as ShopList;
            if (shopl != null)
            {
                listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
            }
        }

        private async void OnChooseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(
                new ProductPage((ShopList)this.BindingContext)
                {
                    BindingContext = new Product()
                }
            );
        }


    }


}
