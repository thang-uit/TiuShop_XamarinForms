using Refit;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiuShop.API;
using TiuShop.DTO;
using TiuShop.View.Popup;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
        }

        private async void btnConfirm_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.edtOldPassword.Text) || string.IsNullOrWhiteSpace(this.edtOldPassword.Text))
            {
                this.lblMessage.Text = App.Current.Resources["lblAlertContent2"].ToString();
                this.edtOldPassword.Focus();
            }
            else if(string.IsNullOrEmpty(this.edtNewPassword.Text) || string.IsNullOrWhiteSpace(this.edtNewPassword.Text))
            {
                this.lblMessage.Text = App.Current.Resources["lblAlertContent2"].ToString();
                this.edtNewPassword.Focus();
            }
            else if(this.edtNewPassword.Text.Length < 6)
            {
                this.lblMessage.Text = App.Current.Resources["lblAlertContent4"].ToString();
                this.edtNewPassword.Focus();
            }
            else if (string.IsNullOrEmpty(this.edtConfirmPassword.Text) || string.IsNullOrWhiteSpace(this.edtConfirmPassword.Text))
            {
                this.lblMessage.Text = App.Current.Resources["lblAlertContent7"].ToString();
                this.edtConfirmPassword.Focus();
            }
            else if (!this.edtNewPassword.Text.Equals(this.edtConfirmPassword.Text))
            {
                this.lblMessage.Text = App.Current.Resources["lblAlertContent10"].ToString();
            }
            else
            {
                await Navigation.PushPopupAsync(new MyLoading());

                AccountRequest account = new AccountRequest();
                account.UserID = Preferences.Get(Common.KEY_USERID, "");
                account.Password = this.edtOldPassword.Text;
                account.NewPassword = this.edtNewPassword.Text;

                var api = RestService.For<IApi>(Common.url);
                var response = await api.ChangePassword(account);

                if (response != null)
                {
                    if (response.Status.Equals(Common.STATUS_SUCCESS))
                    {
                        this.lblMessage.Text = "";
                        await Navigation.PopPopupAsync();
                        await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent23"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await Navigation.PopPopupAsync();
                        this.lblMessage.Text = response.Message;
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
}