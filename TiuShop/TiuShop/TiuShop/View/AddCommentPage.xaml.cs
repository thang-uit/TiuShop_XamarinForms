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
    public partial class AddCommentPage : ContentPage
    {
        private string ID = "";
        public AddCommentPage(string productID)
        {
            InitializeComponent();

            ID = productID;
        }

        private async void btnComment_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.edtComment.Text) || string.IsNullOrWhiteSpace(this.edtComment.Text))
            {
                this.lblMessage.Text = App.Current.Resources["lblAlertContent26"].ToString();
                this.edtComment.Focus();
            }
            else
            {
                await Navigation.PushPopupAsync(new MyLoading());

                var apiResponse = RestService.For<IApi>(Common.url);
                CommentRequest comment = new CommentRequest()
                {
                    UserID = Preferences.Get(Common.KEY_USERID, ""),
                    ProductID = ID,
                    Rating = this.rbRating.SelectedStarValue.ToString(),
                    Content = this.edtComment.Text
                };
                var response = await apiResponse.AddComment(comment);

                if (response != null)
                {
                    if (response.Status.Equals(Common.STATUS_SUCCESS))
                    {
                        await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent27"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
                        await Navigation.PopAsync();
                        await Navigation.PopPopupAsync();
                    }
                }
                else
                {
                    await Navigation.PopPopupAsync();
                    await DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent0"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
                }
            }
        }
    }
}