using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiuShop.API;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuccessPage : ContentPage
    {
        private string myLayout;

        public SuccessPage(string layout)
        {
            InitializeComponent();

            myLayout = layout;
            InitLayout(layout);
        }

        private void InitLayout(string layout)
        {
            if (layout.Equals(Common.LAYOUT_REGISTER))
            {
                this.lblNotice.Text = App.Current.Resources["lblAlertContent11"].ToString();
                this.btnSuccess.Text = App.Current.Resources["btnSuccess1"].ToString(); ;
            } 
            else if(layout.Equals(Common.LAYOUT_ORDER))
            {
                this.lblNotice.Text = App.Current.Resources["lblAlertContent24"].ToString();
                this.btnSuccess.Text = App.Current.Resources["btnSuccess2"].ToString(); ;
            }
        }

        private void btnSuccess_Clicked(object sender, EventArgs e)
        {
            if (myLayout.Equals(Common.LAYOUT_REGISTER))
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            } 
            else if (myLayout.Equals(Common.LAYOUT_ORDER))
            {
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
        }
    }
}