using Refit;
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
    public partial class TabCollectionPage : ContentPage
    {
        public TabCollectionPage()
        {
            InitializeComponent();

            InitCollections();
        }

        private async void InitCollections()
        {
            var apiResponse = RestService.For<IApi>(Common.url);
            var response = await apiResponse.GetCollections();

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    foreach (var img in response.Data)
                    {
                        img.Image = Common.imgUrl + img.Image;
                    }
                    this.clvCollections.ItemsSource = response.Data;
                }
            }
        }

        private void rfvRefresh_Refreshing(object sender, EventArgs e)
        {
            InitCollections();
            this.rfvRefresh.IsRefreshing = false;
        }

        private void clvCollections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}