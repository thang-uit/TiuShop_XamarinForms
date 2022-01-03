using Refit;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using TiuShop.API;
using TiuShop.DTO;
using TiuShop.View.Popup;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TiuShop.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public ICommand loginCommand => new Command(Login);

        public string Username { get; set; }

        public string Passsword { get; set; }

        public string message1;

        private bool flag1 = false;
        private bool flag2 = false;

        public string Message1
        {
            get { return message1; }
            set
            {
                message1 = value;
                OnPropertyChanged();
            }
        }

        public string message2;

        public string Message2
        {
            get { return message2; }
            set
            {
                message2 = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
            {
                return;
            }
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Login()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrWhiteSpace(Username))
            {
                this.Message1 = App.Current.Resources["lblAlertContent1"].ToString();
            }
            else if (Username.Length < 6)
            {
                this.Message1 = App.Current.Resources["lblAlertContent3"].ToString();
            }
            else
            {
                this.Message1 = "";
                flag1 = true;
            }

            if (string.IsNullOrEmpty(Passsword) || string.IsNullOrWhiteSpace(Passsword))
            {
                this.Message2 = App.Current.Resources["lblAlertContent2"].ToString();
            }
            else if (Passsword.Length < 6)
            {
                this.Message2 = App.Current.Resources["lblAlertContent4"].ToString();
            }
            else
            {
                this.Message2 = "";
                flag2 = true;
            }

            if (flag1 && flag2)
            {
                GetResponseAsync();
            }
        }

        private async void GetResponseAsync()
        {
            //await Application.Current.MainPage.DisplayAlert(App.Current.Resources["lblAlert"].ToString(), Username +" | " + Passsword, App.Current.Resources["lblAlertOK"].ToString());
            await Application.Current.MainPage.Navigation.PushPopupAsync(new MyLoading());

            var api = RestService.For<IApi>(Common.url);
            AccountRequest accountRequest = new AccountRequest() { Username = Username, Password = Passsword };
            var response = await api.Login(accountRequest);

            if (response.Status.Equals(Common.STATUS_SUCCESS))
            {
                // Preferences.Set(Common.KEY_USERID, response.Data.UserId);
                await Application.Current.MainPage.Navigation.PopPopupAsync();
                Application.Current.MainPage = new NavigationPage(new MainPage());
                //await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                await Application.Current.MainPage.Navigation.PopPopupAsync();
                await Application.Current.MainPage.DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent5"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
            }
        }
    }
}
