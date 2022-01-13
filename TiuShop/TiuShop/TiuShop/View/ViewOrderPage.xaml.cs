using Refit;
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
    public partial class ViewOrderPage : ContentPage
    {
        private int status = 0;
        public ViewOrderPage(int orderStatus)
        {
            InitializeComponent();

            status = orderStatus;

            InitOrderStatusTitle(orderStatus);
            InitOrderStatus(orderStatus);
        }

        private void InitOrderStatusTitle(int orderStatus)
        {
            if(orderStatus == Common.WAITING_CONFIRM)
            {
                this.Title = App.Current.Resources["lblWaitingConfirm"].ToString();
            } 
            else if(orderStatus == Common.WAITING_GOOD)
            {
                this.Title = App.Current.Resources["lblGoods"].ToString();
            }
            else if (orderStatus == Common.DELIVERING)
            {
                this.Title = App.Current.Resources["lblDelivering"].ToString();
            }
            else if (orderStatus == Common.ORDER_SUCCESS)
            {
                this.Title = App.Current.Resources["lblSuccess"].ToString();
            }
        }

        private async void InitOrderStatus(int orderStatus)
        {
            var apiResponse = RestService.For<IApi>(Common.url);
            UserRequest user = new UserRequest() { UserID = Preferences.Get(Common.KEY_USERID, ""), Status = orderStatus };
            var response = await apiResponse.GetOrderInfo(user);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    this.clvOrder.ItemsSource = response.Data;
                }
            }
        }

        private void rfvRefresh_Refreshing(object sender, EventArgs e)
        {
            InitOrderStatus(status);
            this.rfvRefresh.IsRefreshing = false;
        }

        private void clvOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}