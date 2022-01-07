﻿using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiuShop.API;
using TiuShop.DTO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();

            InitCart();
        }

        private async void InitCart()
        {
            var apiResponse = RestService.For<IApi>(Common.url);
            UserRequest user = new UserRequest() { UserID = Preferences.Get(Common.KEY_USERID, "") };
            var response = await apiResponse.GetCart(user);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    foreach (var img in response.Data)
                    {
                        img.Image[0] = Common.imgUrl + img.Image[0];
                    }
                    this.clvCart.ItemsSource = response.Data;
                }
            }
        }

        private void btnCheckout_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PaymentPage());
        }

        private void clvCart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}