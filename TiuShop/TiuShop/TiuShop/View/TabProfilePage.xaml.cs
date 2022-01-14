using Refit;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiuShop.API;
using TiuShop.DTO;
using TiuShop.Model;
using TiuShop.Util;
using TiuShop.View.Popup;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabProfilePage : ContentPage
    {
        public TabProfilePage()
        {
            InitializeComponent();

            this.swtTheme.IsToggled = Setting.Theme;

            InitUserInfo();
            InitOrderCount();
        }

        private async void InitUserInfo()
        {
            var apiResponse = RestService.For<IApi>(Common.url);
            UserRequest user = new UserRequest() { UserID = Preferences.Get(Common.KEY_USERID, "") };
            var response = await apiResponse.GetUserInfo(user);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    this.lblName.Text = response.Data.Name;
                }
            }
        }

        private async void InitOrderCount()
        {
            var apiResponse = RestService.For<IApi>(Common.url);
            UserRequest user = new UserRequest() { UserID = Preferences.Get(Common.KEY_USERID, "") };
            var response = await apiResponse.GetAmountOrder(user);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    this.lblAmount1.Text = response.Data.Order0.ToString();
                    this.lblAmount2.Text = response.Data.Order1.ToString();
                    this.lblAmount3.Text = response.Data.Order2.ToString();
                    this.lblAmount4.Text = response.Data.Order3.ToString();
                    this.lblAmount5.Text = response.Data.Order4.ToString();
                }
            }
        }

        private void rfvRefresh_Refreshing(object sender, EventArgs e)
        {
            InitUserInfo();
            InitOrderCount();
            this.rfvRefresh.IsRefreshing = false;
        }

        private void tapCart_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CartPage());
        }

        private void tapWishList_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WishListPage());
        }

        private async  void tapLogout_Tapped(object sender, EventArgs e)
        {
            var option = await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent20"].ToString(), App.Current.Resources["lblAlertOK"].ToString(), App.Current.Resources["lblAlertCancel"].ToString());

            if (option)
            {
                await Navigation.PushPopupAsync(new MyLoading());

                Preferences.Remove(Common.KEY_USERID);
                bool key = Preferences.ContainsKey(Common.KEY_USERID);
                if (key)
                {
                    await Navigation.PopPopupAsync();
                    await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent0"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    await Navigation.PopPopupAsync();
                }
            }
        }

        private void tapUserInfo_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserInfomationPage());
        }

        private void tapOrderCanceled_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewOrderPage(Common.ORDER_CANCEL));
        }

        private void tapChangePassword_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePasswordPage());
        }

        private void tapRating_Tapped(object sender, EventArgs e)
        {

        }

        private void tapOrder0_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewOrderPage(Common.WAITING_CONFIRM));
        }

        private void tapOrder1_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewOrderPage(Common.WAITING_GOOD));
        }

        private void tapOrder2_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewOrderPage(Common.DELIVERING));
        }

        private void tapOrder3_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewOrderPage(Common.ORDER_SUCCESS));
        }

        private void swtTheme_Toggled(object sender, ToggledEventArgs e)
        {
            Setting.Theme = this.swtTheme.IsToggled;
            TheTheme.SetTheme();
        }
    }
}