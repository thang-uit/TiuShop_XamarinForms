using Refit;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiuShop.API;
using TiuShop.DTO;
using TiuShop.View.Popup;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        private string ID = "";
        private List<string> sizeList = new List<string>();

        public ProductDetailPage(string productID)
        {
            InitializeComponent();

            ID = productID;

            InitSize();
            InnitProductDetail(productID);
        }

        private void InitSize()
        {
            this.sizeList.Add("S");
            this.sizeList.Add("M");
            this.sizeList.Add("L");
            this.sizeList.Add("XL");
            this.sizeList.Add("XXL");
            this.sizeList.Add("XXXL");
            this.picSize.ItemsSource = this.sizeList;
            this.picSize.SelectedIndex = 3;
        }

        private async void InnitProductDetail(string productID)
        {
            await Navigation.PushPopupAsync(new MyLoading());

            var apiResponse = RestService.For<IApi>(Common.url);
            CartRequest cart = new CartRequest() { UserID = Preferences.Get(Common.KEY_USERID, ""), ProductID = productID };
            var response = await apiResponse.GetProductDetail(cart);

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

                    if(response.Data.IsWishList)
                    {
                        this.imgWishList.Source = "ic_heartfull.png";
                    }
                    else
                    {
                        this.imgWishList.Source = "ic_heart.png";
                    }

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

        private async void btnAddToCart_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new MyLoading());

            var selectedItem = this.picSize.SelectedItem as string;

            var apiResponse = RestService.For<IApi>(Common.url);
            CartRequest wishList = new CartRequest() { UserID = Preferences.Get(Common.KEY_USERID, ""), ProductID = ID, Size = selectedItem.ToString(), Quantity = this.lblAmount.Text };
            var response = await apiResponse.AddToCart(wishList);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    await Navigation.PopPopupAsync();
                    await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent14"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
                }
                else
                {
                    await Navigation.PopPopupAsync();
                    await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent0"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
                }
            }
            else
            {
                await Navigation.PopPopupAsync();
                await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent0"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
            }
        }

        private void btnWriteComment_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddCommentPage());
        }

        private async void tapWishList_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new MyLoading());

            var apiResponse = RestService.For<IApi>(Common.url);
            CartRequest wishList = new CartRequest() { UserID = Preferences.Get(Common.KEY_USERID, ""), ProductID = ID};
            var response = await apiResponse.HandleWishList(wishList);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    if (response.Message.Equals("1"))
                    {
                        this.imgWishList.Source = "ic_heartfull.png";
                        await Navigation.PopPopupAsync();
                        await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent12"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
                    } 
                    else if(response.Message.Equals("3"))
                    {
                        this.imgWishList.Source = "ic_heart.png";
                        await Navigation.PopPopupAsync();
                        await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent13"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
                    }
                    else
                    {
                        await Navigation.PopPopupAsync();
                        await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent0"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
                    }
                }
            }
            else
            {
                await Navigation.PopPopupAsync();
                await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent0"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
            }
        }

        private void picSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var picked = (Picker)sender;
            //int selected = picked.SelectedIndex;
            //if (selected < 0)
            //{
            //    DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent0"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
            //}
        }
    }
}