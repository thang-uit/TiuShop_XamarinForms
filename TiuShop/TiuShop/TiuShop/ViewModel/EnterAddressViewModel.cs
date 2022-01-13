using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TiuShop.Model;
using TiuShop.View;
using Xamarin.Forms;

namespace TiuShop.ViewModel
{
    public class EnterAddressViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
            {
                return;
            }
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand checkOutCommand => new Command(CheckOut);

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Note { get; set; }

        private string message1;
        public string Message1
        {
            get { return message1; }
            set
            {
                message1 = value;
                OnPropertyChanged();
            }
        }

        private string message2;
        public string Message2
        {
            get { return message2; }
            set
            {
                message2 = value;
                OnPropertyChanged();
            }
        }

        private string message3;
        public string Message3
        {
            get { return message3; }
            set
            {
                message3 = value;
                OnPropertyChanged();
            }
        }

        private string message4;
        public string Message4
        {
            get { return message4; }
            set
            {
                message4 = value;
                OnPropertyChanged();
            }
        }

        private bool flag1 = false;
        private bool flag2 = false;
        private bool flag3 = false;
        private bool flag4 = false;

        public INavigation Navigation { get; set; }

        public EnterAddressViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
        }

        private void CheckOut()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
            {
                this.Message1 = App.Current.Resources["lblAlertContent16"].ToString();
            }
            else
            {
                this.Message1 = "";
                flag1 = true;
            }

            if (string.IsNullOrEmpty(Phone) || string.IsNullOrWhiteSpace(Phone))
            {
                this.Message2 = App.Current.Resources["lblAlertContent17"].ToString();
            }
            else
            {
                this.Message2 = "";
                flag2 = true;
            }

            if (string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email))
            {
                this.Message3 = App.Current.Resources["lblAlertContent18"].ToString();
            }
            else
            {
                this.Message3 = "";
                flag3 = true;
            }

            if (string.IsNullOrEmpty(Address) || string.IsNullOrWhiteSpace(Address))
            {
                this.Message4 = App.Current.Resources["lblAlertContent19"].ToString();
            }
            else
            {
                this.Message4 = "";
                flag4 = true;
            }

            if (flag1 && flag2 && flag3 && flag4)
            {
                Continue();
            }
        }

        private void Continue()
        {
            User user = new User() { Name = Name, Phone = Phone, Email = Email, Address = Address };
            Navigation.PushAsync(new OrderPage(user, Note));
        }
    }
}
