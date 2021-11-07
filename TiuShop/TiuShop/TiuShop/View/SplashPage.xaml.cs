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
        private Image imgLogo;
        public SplashPage()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            var layout = new AbsoluteLayout();
            imgLogo = new Image { Source = "logo200_200.png", WidthRequest = 100, HeightRequest = 100 };

            NavigationPage.SetHasNavigationBar(this, false);

            AbsoluteLayout.SetLayoutFlags(imgLogo, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(imgLogo, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            layout.Children.Add(imgLogo);
            this.BackgroundColor = Color.FromHex("#000000");
            this.Content = layout;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await imgLogo.ScaleTo(1, 2000);
            await imgLogo.ScaleTo(0.8, 1500, Easing.Linear);
            await imgLogo.ScaleTo(250, 2800, Easing.Linear);
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}