
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiuShop.API;
using TiuShop.Model;
using TiuShop.View.Popup;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabShopPage : ContentPage
    {
        public TabShopPage()
        {
            InitializeComponent();

            InitSlider();
            InitNewProduct();
            InitSaleProduct();
        }

        //private void Init()
        //{
        //    var userID = Preferences.Get(Common.KEY_USERID, "");
        //}

        private void tapCart_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CartPage());
        }

        private async void InitSlider()
        {
            var apiResponse = RestService.For<IApi>(Common.url);
            var response = await apiResponse.GetSlider(5);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    foreach(var img in response.Data)
                    {
                        img.SliderImg = Common.imgUrl + img.SliderImg;
                    }    
                    this.slider.ItemsSource = response.Data;
                }
            }
        }

        private async void InitNewProduct()
        {
            var apiResponse = RestService.For<IApi>(Common.url);
            var response = await apiResponse.GetGroupProduct(Common.NEW_PRODUCT, 5);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    foreach (var img in response.Data)
                    {
                        img.Image[0] = Common.imgUrl + img.Image[0];
                    }
                    this.clvNewProduct.ItemsSource = response.Data;
                }
            }
        }

        private async void InitSaleProduct()
        {
            var apiResponse = RestService.For<IApi>(Common.url);
            var response = await apiResponse.GetGroupProduct(Common.DISCOUNT_PRODUCT, 5);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    foreach (var img in response.Data)
                    {
                        img.Image[0] = Common.imgUrl + img.Image[0];
                    }
                    this.clvSaleProduct.ItemsSource = response.Data;
                }
            }
        }

        private async void tapMan_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GroupProduct(Common.MAN_PRODUCT, null));
        }

        private async void tapWoman_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GroupProduct(Common.WOMAN_PRODUCT, null));
        }

        private async void tapBoth_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GroupProduct(Common.BOTH_PRODUCT, null));
        }

        private void rfvRefresh_Refreshing(object sender, EventArgs e)
        {
            InitSlider();
            InitNewProduct();
            InitSaleProduct();
            this.rfvRefresh.IsRefreshing = false;
        }

        private async void clvNewProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = e.CurrentSelection.FirstOrDefault() as Product;
            if (product == null)
            {
                return;
            }
            await Navigation.PushAsync(new ProductDetailPage(product.ProductId));
        }

        private async void clvSaleProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = e.CurrentSelection.FirstOrDefault() as Product;
            if (product == null)
            {
                return;
            }
            await Navigation.PushAsync(new ProductDetailPage(product.ProductId));
        }

        private void tapWishList_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WishListPage());
        }

        private void tapLogo_Tapped(object sender, EventArgs e)
        {
            InitSlider();
            InitNewProduct();
            InitSaleProduct();
        }
    }
}