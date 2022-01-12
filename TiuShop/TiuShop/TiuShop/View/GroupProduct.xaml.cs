using Refit;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiuShop.API;
using TiuShop.Model;
using TiuShop.View.Popup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupProduct : ContentPage
    {
        private string baseLayout = "";
        private Collections _collections;

        public GroupProduct(string layout, Collections collections)
        {
            InitializeComponent();

            baseLayout = layout;
            _collections = collections;

            Navigation.PushPopupAsync(new MyLoading());

            if (layout.Equals(Common.MAN_PRODUCT))
            {
                this.Title = App.Current.Resources["lblMan"].ToString();
                InitManProduct();
            }
            else if (layout.Equals(Common.WOMAN_PRODUCT))
            {
                this.Title = App.Current.Resources["lblWoman"].ToString();
                InitWomanProduct();
            }
            else if (layout.Equals(Common.BOTH_PRODUCT))
            {
                this.Title = App.Current.Resources["lblBoth"].ToString();
                InitBothProduct();
            } 
            else if(collections != null)
            {
                this.Title = collections.Name;
                InitCollectionsProduct(collections);
            }

            Navigation.PopPopupAsync();
        }

        private async void InitManProduct()
        {
            var apiResponse = RestService.For<IApi>(Common.url);
            var response = await apiResponse.GetGroupProduct(Common.MAN_PRODUCT, 0);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    foreach (var img in response.Data)
                    {
                        img.Image[0] = Common.imgUrl + img.Image[0];
                    }
                    this.clvGroupProduct.ItemsSource = response.Data;
                }
            }
        }

        private async void InitWomanProduct()
        {
            var apiResponse = RestService.For<IApi>(Common.url);
            var response = await apiResponse.GetGroupProduct(Common.WOMAN_PRODUCT, 0);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    foreach (var img in response.Data)
                    {
                        img.Image[0] = Common.imgUrl + img.Image[0];
                    }
                    this.clvGroupProduct.ItemsSource = response.Data;
                }
            }
        }

        private async void InitBothProduct()
        {
            var apiResponse = RestService.For<IApi>(Common.url);
            var response = await apiResponse.GetGroupProduct(Common.BOTH_PRODUCT, 0);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    foreach (var img in response.Data)
                    {
                        img.Image[0] = Common.imgUrl + img.Image[0];
                    }
                    this.clvGroupProduct.ItemsSource = response.Data;
                }
            }
        }

        private async void InitCollectionsProduct(Collections collections)
        {
            var apiResponse = RestService.For<IApi>(Common.url);
            var response = await apiResponse.GetCollectionsProduct(collections.CollectionsID);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    foreach (var img in response.Data)
                    {
                        img.Image[0] = Common.imgUrl + img.Image[0];
                    }
                    this.clvGroupProduct.ItemsSource = response.Data;
                }
            }
        }

        private void rfvRefresh_Refreshing(object sender, EventArgs e)
        {
            if (baseLayout.Equals(Common.MAN_PRODUCT))
            {
                InitManProduct();
            }
            else if (baseLayout.Equals(Common.WOMAN_PRODUCT))
            {
                InitWomanProduct();
            }
            else if (baseLayout.Equals(Common.BOTH_PRODUCT))
            {
                InitBothProduct();
            }
            else if (_collections != null)
            {
                InitCollectionsProduct(_collections);
            }

            this.rfvRefresh.IsRefreshing = false;
        }

        private async void clvGroupProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = e.CurrentSelection.FirstOrDefault() as Product;
            if (product == null)
            {
                return;
            }
            await Navigation.PushAsync(new ProductDetailPage(product.ProductId));
        }
    }
}