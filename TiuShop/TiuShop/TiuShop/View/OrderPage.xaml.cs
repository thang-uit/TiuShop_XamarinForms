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
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        private User mUser;
        private string mNote;

        private decimal totalPrice = 0;

        public OrderPage(User user, string note)
        {
            InitializeComponent();

            mUser = user;
            mNote = note;

            InitUser(user, note);
            InitProduct();
        }

        private void InitUser(User user, string note)
        {
            this.lblName.Text = user.Name;
            this.lblPhone.Text = user.Phone;
            this.lblEmail.Text = user.Email;
            this.lblAddress.Text = user.Address;
            this.lblNote.Text = note;
        }

        private async void InitProduct()
        {
            await Navigation.PushPopupAsync(new MyLoading());

            var apiResponse = RestService.For<IApi>(Common.url);
            CartRequest cart = new CartRequest() { UserID = Preferences.Get(Common.KEY_USERID, "") };
            var response = await apiResponse.GetCart(cart);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    foreach (var data in response.Data)
                    {
                        data.Image[0] = Common.imgUrl + data.Image[0];
                        data.FinalPrice = data.FinalPrice.Replace(".", "");
                        totalPrice = totalPrice + (Convert.ToDecimal(data.Quantity) * Convert.ToDecimal(data.FinalPrice));
                    }
                    this.clvItems.ItemsSource = response.Data;
                    this.lblTotalPrice.Text = totalPrice.ToString();

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

        private async void btnOrder_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new MyLoading());

            var apiResponse = RestService.For<IApi>(Common.url);
            OrderRequest order = new OrderRequest()
            {
                UserID = Preferences.Get(Common.KEY_USERID, ""),
                Name = mUser.Name,
                Email = mUser.Email,
                Phone = mUser.Phone,
                Address = mUser.Address,
                Note = mNote,
                TotalPrice = this.lblTotalPrice.Text
            };

            var response = await apiResponse.AddNewOrder(order);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new SuccessPage(Common.LAYOUT_ORDER));
                    await Navigation.PopPopupAsync();
                }
                else
                {
                    await Navigation.PopPopupAsync();
                    await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent0"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
                }
            }
            else
            {
                await Navigation.PopPopupAsync();
                await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent0"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
            }
        }
    }
}