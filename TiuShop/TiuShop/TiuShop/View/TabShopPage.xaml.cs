﻿
using Refit;
using Rg.Plugins.Popup.Extensions;
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

            Init();
            InitSlider();
        }

        private void Init()
        {
            var userID = Preferences.Get(Common.KEY_USERID, "");

            //List<Banner> banners = new List<Banner>()
            //{
            //    new Banner() { bannerID = 2, bannerImage = "slider2.jpg", productID = 2 },
            //    new Banner() { bannerID = 1, bannerImage = "slider1.jpg", productID = 1 },
            //    new Banner() { bannerID = 3, bannerImage = "slider3.jpg", productID = 3 },
            //    new Banner() { bannerID = 4, bannerImage = "slider4.jpg", productID = 4 },
            //    new Banner() { bannerID = 5, bannerImage = "bgregister.jpg", productID = 5 },
            //};

            //var httpClient = new HttpClient();
            //var result = await httpClient.GetStringAsync("https://imusicapi.000webhostapp.com/Server/slider.php");

            //var resultSlider = JsonConvert.DeserializeObject<Model.Slider[]>(result);

            //this.slider.ItemsSource = resultSlider;

            //var test = await httpClient.GetStringAsync("http://192.168.1.2/Code/TiuShop/abc.txt");
            //this.lblTest.Text = test.ToString();
            
        }

        private void tapProduct_Tapped(object sender, EventArgs e)
        {
            //DisplayAlert("Alert", "Bất ngờ lắm phải không", "Bất ngờ");
            //Navigation.PushPopupAsync(new MyLoading());
            Navigation.PushAsync(new ProductDetailPage());
        }

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

        private void tapMan_Tapped(object sender, EventArgs e)
        {

        }

        private void tapWoman_Tapped(object sender, EventArgs e)
        {

        }

        private void tapBoth_Tapped(object sender, EventArgs e)
        {

        }
    }
}