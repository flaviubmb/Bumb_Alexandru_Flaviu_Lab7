using System;
using BumbAlexandruFlaviuLab7.Models;

namespace BumbAlexandruFlaviuLab7
{
    public partial class ProductPage : ContentPage
    {
        ShopList sl;
        public ProductPage(ShopList slist)
        {
            InitializeComponent();
            sl = slist;
        }
    
      
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var product = (Product)BindingContext;
            if (product != null)
            {
                await App.Database.SaveProductAsync(product);
                listView.ItemsSource = await App.Database.GetProductsAsync();
            }
        }

       
        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var product = listView.SelectedItem as Product;
            if (product != null)
            {
                await App.Database.DeleteProductAsync(product);
                listView.ItemsSource = await App.Database.GetProductsAsync();
            }
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                var p = listView.SelectedItem as Product;
                if (p != null)
                {
                    var lp = new ListProduct
                    {
                        ShopListID = sl.ID,
                        ProductID = p.ID
                    };

                    await App.Database.SaveListProductAsync(lp);

                 
                    p.ListProducts = new List<ListProduct> { lp };

                   
                    await Navigation.PopAsync();
                }
            }
        }




        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }
    }
}
