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
    public partial class TabShopPage : ContentPage
    {
        public TabShopPage()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            List<Banner> banners = new List<Banner>()
            {
                new Banner() { bannerID = 2, bannerImage = "slider2.jpg", productID = 2 },
                new Banner() { bannerID = 1, bannerImage = "slider1.jpg", productID = 1 },
                new Banner() { bannerID = 3, bannerImage = "slider3.jpg", productID = 3 },
                new Banner() { bannerID = 4, bannerImage = "slider4.jpg", productID = 4 },
                new Banner() { bannerID = 5, bannerImage = "bgregister.jpg", productID = 5 },
            };

            this.slider.ItemsSource = banners;
        }
    }
}