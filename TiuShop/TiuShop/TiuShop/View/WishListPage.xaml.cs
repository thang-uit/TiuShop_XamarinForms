using System;
using TiuShop.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WishListPage : ContentPage
    {
        private HandleWishListViewModel handleWishListViewModel;

        public WishListPage()
        {
            InitializeComponent();

            BindingContext = handleWishListViewModel = new HandleWishListViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            handleWishListViewModel.OnAppearing();
        }
    }
}