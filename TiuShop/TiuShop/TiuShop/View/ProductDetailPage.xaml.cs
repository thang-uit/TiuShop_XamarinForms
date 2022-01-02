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
    public partial class ProductDetailPage : ContentPage
    {
        public ProductDetailPage()
        {
            InitializeComponent();
        }

        private void stpAmount_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            this.lblAmount.Text = e.NewValue.ToString();
        }

        private void btnAddToCart_Clicked(object sender, EventArgs e)
        {

        }

        private void btnWriteComment_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddCommentPage());
        }
    }
}