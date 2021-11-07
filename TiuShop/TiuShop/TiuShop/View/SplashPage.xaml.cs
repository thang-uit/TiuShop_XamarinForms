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
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
        }

        //private void Init()
        //{
        //    //var layout = new AbsoluteLayout();
           
        //    NavigationPage.SetHasNavigationBar(this, false);

        //    //AbsoluteLayout.SetLayoutFlags(imgLogo, AbsoluteLayoutFlags.PositionProportional);
        //    //AbsoluteLayout.SetLayoutBounds(imgLogo, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

        //    //layout.Children.Add(imgLogo);
        //    //this.BackgroundColor = (Color) Application.Current.Resources["ColorMain3"];
        //    //this.Content = layout;
        //}

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Source: https://youtu.be/y8ce7OyAgYU

            await this.imgLogo.ScaleTo(1, 2000);
            await this.imgLogo.ScaleTo(0.8, 1500, Easing.Linear);
            await this.imgLogo.ScaleTo(200, 2500, Easing.Linear);
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}