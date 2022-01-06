using Refit;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiuShop.API;
using TiuShop.View.Popup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        private string ID = "";

        public ProductDetailPage(string productID)
        {
            InitializeComponent();

            ID = productID;

            InnitProductDetail(productID);
        }

        private async void InnitProductDetail(string productID)
        {
            await Navigation.PushPopupAsync(new MyLoading());

            var apiResponse = RestService.For<IApi>(Common.url);
            var response = await apiResponse.GetProductDetail(productID);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    for (var i = 0; i < response.Data.Image.Count; i++)
                    {
                        response.Data.Image[i] = Common.imgUrl + response.Data.Image[i];
                        Console.WriteLine("Image: " + response.Data.Image[i]);
                    }
                    this.slider.ItemsSource = response.Data.Image;

                    var sale = !response.Data.Sale.Equals("0%") ? " (" + response.Data.Sale + ")" : "";

                    this.lblProductName.Text = response.Data.Name + sale;

                    if (response.Data.Sale.Equals("0%"))
                    {
                        this.lblProductPrice.IsVisible = false;
                    }
                    else
                    {
                        this.lblProductPrice.Text = response.Data.Price;
                    }
                    
                    this.lblProductFinalPrice.Text = response.Data.FinalPrice;
                    this.lblProductDescription.Text = response.Data.Description;
                }
            }
            await Navigation.PopPopupAsync();
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

        private void rfvRefresh_Refreshing(object sender, EventArgs e)
        {
            InnitProductDetail(ID);
            this.rfvRefresh.IsRefreshing = false;
        }
    }
}