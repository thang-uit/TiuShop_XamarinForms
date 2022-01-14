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
using TiuShop.View.Popup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewOrderDetailPage : ContentPage
    {
        private Order mOrder;
        public ViewOrderDetailPage(Order order)
        {
            InitializeComponent();

            mOrder = order;

            InitTitle(order);
            InitInfo(order);
        }

        private void InitInfo(Order order)
        {
            if(Convert.ToInt32(order.Status) == Common.WAITING_CONFIRM)
            {
                this.btnCancelOrder.IsVisible = true;
            }
            this.lblName.Text = order.Name;
            this.lblEmail.Text = order.Email;
            this.lblPhone.Text = order.Phone;
            this.lblAddress.Text = order.Address;
            this.lblNote.Text = order.Note;

            if (order.Payment.Equals("0"))
            {
                this.lblPayment.Text = App.Current.Resources["lblPayment1"].ToString();
            }
            this.lblDate.Text = order.Date;
            this.lblDateSuccess.Text = order.DateSuccess;

            if(Convert.ToInt32(order.Status) == Common.ORDER_SUCCESS)
            {
                this.lblFrameDateSuccess.IsVisible = true;
            }
            this.lblTotalPrice.Text = order.TotalPrice.ToString();

            foreach(var img in order.Product)
            {
                img.Image[0] = Common.imgUrl + img.Image[0];
            }
            this.clvItems.ItemsSource = order.Product;

        }

        private void InitTitle(Order order)
        {
            if(Convert.ToInt32(order.Status) == Common.WAITING_CONFIRM)
            {
                this.Title = App.Current.Resources["lblWaitingConfirm"].ToString();
            } 
            else if (Convert.ToInt32(order.Status) == Common.WAITING_GOOD)
            {
                this.Title = App.Current.Resources["lblGoods"].ToString();
            }
            else if (Convert.ToInt32(order.Status) == Common.DELIVERING)
            {
                this.Title = App.Current.Resources["lblDelivering"].ToString();
            }
            else if (Convert.ToInt32(order.Status) == Common.ORDER_SUCCESS)
            {
                this.Title = App.Current.Resources["lblSuccess"].ToString();
            }
        }

        private async void btnCancelOrder_Clicked(object sender, EventArgs e)
        {
            var option = await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent25"].ToString(), App.Current.Resources["lblAlertOK"].ToString(), App.Current.Resources["lblAlertCancel"].ToString());

            if (option)
            {
                await Navigation.PushPopupAsync(new MyLoading());

                var apiResponse = RestService.For<IApi>(Common.url);
                OrderRequest order = new OrderRequest() { OrderID = mOrder.OrderId, Status = Common.ORDER_CANCEL };
                var response = await apiResponse.UpdateOrderStatus(order);

                if (response != null)
                {
                    if (response.Status.Equals(Common.STATUS_SUCCESS))
                    {
                        Application.Current.MainPage = new NavigationPage(new MainPage());
                        await Navigation.PopPopupAsync();
                    }
                    else
                    {
                        await Navigation.PopPopupAsync();
                    }
                }
                else
                {
                    await Navigation.PopPopupAsync();
                }
            }
        }

        private async void clvItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = e.CurrentSelection.FirstOrDefault() as Cart;
            if (product == null)
            {
                return;
            }
            await Navigation.PushAsync(new ProductDetailPage(product.ProductId));
        }
    }
}