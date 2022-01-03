using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TiuShop.ViewModel
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Username { get; set; }

        public string Displayname { get; set; }

        public string Passsword { get; set; }

        public string ConfirmPasssword { get; set; }

        public string Message { get; set; }
    }
}
