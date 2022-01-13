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
    public partial class UserInfomationPage : ContentPage
    {
        private List<string> genderList = new List<string>();

        private bool flag1 = false;
        private bool flag2 = false;
        private bool flag3 = false;
        private bool flag4 = false;

        public UserInfomationPage()
        {
            InitializeComponent();

            InitSize();
            InitUserInfo();
        }

        private void InitSize()
        {
            this.genderList.Add(App.Current.Resources["lblMale"].ToString());
            this.genderList.Add(App.Current.Resources["lblFeMale"].ToString());
            this.picGender.ItemsSource = this.genderList;
            this.picGender.SelectedIndex = 0;
        }

        private async void InitUserInfo()
        {
            await Navigation.PushPopupAsync(new MyLoading());

            var apiResponse = RestService.For<IApi>(Common.url);
            UserRequest user = new UserRequest() { UserID = Preferences.Get(Common.KEY_USERID, "") };
            var response = await apiResponse.GetUserInfo(user);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    this.edtName.Text = response.Data.Name;
                    this.edtPhone.Text = response.Data.Phone;
                    this.edtEmail.Text = response.Data.Email;
                    this.picGender.SelectedIndex = Convert.ToInt32(response.Data.Gender);
                    this.edtAddress.Text = response.Data.Address;

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

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(edtName.Text) || string.IsNullOrWhiteSpace(edtName.Text))
            {
                this.lblMessage1.Text = App.Current.Resources["lblAlertContent16"].ToString();
            }
            else
            {
                this.lblMessage1.Text = "";
                flag1 = true;
            }

            if (string.IsNullOrEmpty(edtPhone.Text) || string.IsNullOrWhiteSpace(edtPhone.Text))
            {
                this.lblMessage2.Text = App.Current.Resources["lblAlertContent17"].ToString();
            }
            else
            {
                this.lblMessage2.Text = "";
                flag2 = true;
            }

            if (string.IsNullOrEmpty(edtEmail.Text) || string.IsNullOrWhiteSpace(edtEmail.Text))
            {
                this.lblMessage3.Text = App.Current.Resources["lblAlertContent18"].ToString();
            }
            else
            {
                this.lblMessage3.Text = "";
                flag3 = true;
            }

            if (string.IsNullOrEmpty(edtAddress.Text) || string.IsNullOrWhiteSpace(edtAddress.Text))
            {
                this.lblMessage4.Text = App.Current.Resources["lblAlertContent19"].ToString();
            }
            else
            {
                this.lblMessage4.Text = "";
                flag4 = true;
            }

            if (flag1 && flag2 && flag3 && flag4)
            {
                UserRequest user = new UserRequest();
                user.UserID = Preferences.Get(Common.KEY_USERID, "");
                user.Name = edtName.Text;
                user.Phone = edtPhone.Text;
                user.Email = edtEmail.Text;
                user.Gender = picGender.SelectedIndex.ToString();
                user.Address = edtAddress.Text;

                await Navigation.PushPopupAsync(new MyLoading());

                var apiResponse = RestService.For<IApi>(Common.url);
                var response = await apiResponse.UpdateUserInfo(user);

                if (response != null)
                {
                    if (response.Status.Equals(Common.STATUS_SUCCESS))
                    {
                        await Navigation.PopPopupAsync();
                        await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent21"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
                    }
                    else
                    {
                        await Navigation.PopPopupAsync();
                        await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent22"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
                    }
                }
                else
                {
                    await Navigation.PopPopupAsync();
                    await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent21"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
                }
            }
        }
    }
}