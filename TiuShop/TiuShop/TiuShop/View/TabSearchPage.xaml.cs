using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiuShop.API;
using TiuShop.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabSearchPage : ContentPage
    {
        public TabSearchPage()
        {
            InitializeComponent();

            InitCategory();
            InitRandomProduct();
        }

        private async void InitRandomProduct()
        {
            var apiResponse = RestService.For<IApi>(Common.url);
            var response = await apiResponse.GetGroupProduct(Common.ALL_PRODUCT, 5);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    foreach (var img in response.Data)
                    {
                        img.Image[0] = Common.imgUrl + img.Image[0];
                    }
                    this.clvProduct.ItemsSource = response.Data;
                }
            }
        }

        private async void InitCategory()
        {
            var apiResponse = RestService.For<IApi>(Common.url);
            var response = await apiResponse.GetCategory();

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    this.clvCategory.ItemsSource = response.Data;
                }
            }
        }

        private void rfvRefresh_Refreshing(object sender, EventArgs e)
        {
            InitCategory();
            InitRandomProduct();
            this.rfvRefresh.IsRefreshing = false;
        }

        private async void clvProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = e.CurrentSelection.FirstOrDefault() as Product;
            if (product == null)
            {
                return;
            }
            await Navigation.PushAsync(new ProductDetailPage(product.ProductId));
        }

        private async void edtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var apiResponse = RestService.For<IApi>(Common.url);
            var response = await apiResponse.SearchProduct(e.NewTextValue);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    foreach (var img in response.Data)
                    {
                        img.Image[0] = Common.imgUrl + img.Image[0];
                    }
                    this.clvProduct.ItemsSource = response.Data;
                }
            }
        }

        private async void clvCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var category = e.CurrentSelection.FirstOrDefault() as Category;
            if (category == null)
            {
                return;
            }

            var apiResponse = RestService.For<IApi>(Common.url);
            var response = await apiResponse.GetCategoryProduct(category.CategoryID);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    foreach (var img in response.Data)
                    {
                        img.Image[0] = Common.imgUrl + img.Image[0];
                    }
                    this.clvProduct.ItemsSource = response.Data;
                }
            }
        }
    }
}