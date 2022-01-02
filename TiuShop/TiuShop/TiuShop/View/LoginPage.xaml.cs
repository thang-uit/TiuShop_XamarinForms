using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        //private void btnLogin_Clicked(object sender, EventArgs e)
        //{
        //    //var username = this.edtUsername.Text;
        //    //var password = this.edtPassword.Text;

        //    //if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
        //    //{
        //    //    DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent1"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
        //    //    this.edtUsername.Focus();
        //    //}
        //    //else if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
        //    //{
        //    //    DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent2"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
        //    //    this.edtPassword.Focus();
        //    //}
        //    //else if(username.Length < 6)
        //    //{
        //    //    DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent3"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
        //    //    this.edtUsername.Focus();
        //    //}
        //    //else if (password.Length < 6)
        //    //{
        //    //    DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent4"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
        //    //    this.edtPassword.Focus();
        //    //}
        //    //else
        //    //{

        //    //}


        //    //Application.Current.MainPage = new NavigationPage(new MainPage());
        //    //Navigation.PushPopupAsync(new MyLoading());


        //    //Navigation.PushAsync(new MainPage());
        //    //Navigation.PushPopupAsync(new MyPopup("Notification", "Do you want to log out?", "Yes"));
        //}

        private void tapRegister_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}