using Refit;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TiuShop.API;
using TiuShop.DTO;
using TiuShop.View;
using TiuShop.View.Popup;
using Xamarin.Forms;

namespace TiuShop.ViewModel
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
            {
                return;
            }
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Username { get; set; }

        public string Displayname { get; set; }

        public string Passsword { get; set; }

        public string ConfirmPasssword { get; set; }

        private string message;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        public ICommand registerCommand => new Command(Register);

        private void Register()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrWhiteSpace(Username))
            {
                this.Message = App.Current.Resources["lblAlertContent1"].ToString();
            }
            else if (Username.Length < 6)
            {
                this.Message = App.Current.Resources["lblAlertContent3"].ToString();
            }
            else if (string.IsNullOrEmpty(Displayname) || string.IsNullOrWhiteSpace(Displayname))
            {
                this.Message = App.Current.Resources["lblAlertContent6"].ToString();
            }
            else if (Displayname.Length < 6)
            {
                this.Message = App.Current.Resources["lblAlertContent8"].ToString();
            }
            else if (string.IsNullOrEmpty(Passsword) || string.IsNullOrWhiteSpace(Passsword))
            {
                this.Message = App.Current.Resources["lblAlertContent2"].ToString();
            }
            else if (Passsword.Length < 6)
            {
                this.Message = App.Current.Resources["lblAlertContent4"].ToString();
            }
            else if (string.IsNullOrEmpty(ConfirmPasssword) || string.IsNullOrWhiteSpace(ConfirmPasssword))
            {
                this.Message = App.Current.Resources["lblAlertContent7"].ToString();
            }
            else if (!Passsword.Equals(ConfirmPasssword))
            {
                this.Message = App.Current.Resources["lblAlertContent10"].ToString();
            }
            else
            {
                GetResponseAsync();
            }
        }

        private async void GetResponseAsync()
        {
            await Application.Current.MainPage.Navigation.PushPopupAsync(new MyLoading());

            var api = RestService.For<IApi>(Common.url);
            AccountRequest accountRequest = new AccountRequest() { Username = Username, Password = Passsword, Name = Displayname };
            var response = await api.Register(accountRequest);

            if(response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    this.Message = "";
                    await Application.Current.MainPage.Navigation.PushAsync(new SuccessPage(Common.LAYOUT_REGISTER));
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                    this.Message = response.Message;
                }
            }
            else
            {
                await Application.Current.MainPage.Navigation.PopPopupAsync();
                await Application.Current.MainPage.DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent0"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
            }
        }
    }
}
