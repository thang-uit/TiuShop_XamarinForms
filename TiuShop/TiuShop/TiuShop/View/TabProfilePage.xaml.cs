using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiuShop.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabProfilePage : ContentPage
    {
        public TabProfilePage()
        {
            InitializeComponent();
        }

        private void lvOptions_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void tapCart_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CartPage());
        }
    }
}