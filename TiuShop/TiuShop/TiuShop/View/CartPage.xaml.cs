using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiuShop.API;
using TiuShop.DTO;
using TiuShop.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        private HandleCartViewModel handleCartViewModel;

        public CartPage()
        {
            InitializeComponent();

            BindingContext = handleCartViewModel = new HandleCartViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            handleCartViewModel.OnAppearing();
        }

        private void btnCheckout_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EnterAddressPage());
        }
    }
}