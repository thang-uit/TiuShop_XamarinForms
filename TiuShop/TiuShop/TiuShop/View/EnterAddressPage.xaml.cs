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
using TiuShop.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnterAddressPage : ContentPage
    {
        private EnterAddressViewModel viewModel;

        public EnterAddressPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new EnterAddressViewModel(Navigation);

            InitUserInfo();
        }

        private async void InitUserInfo()
        {
            await Navigation.PushPopupAsync(new MyLoading());

            var apiResponse = RestService.For<IApi>(Common.url);
            UserRequest user = new UserRequest() { UserID = Preferences.Get(Common.KEY_USERID, "") };
            var response = await apiResponse.GetUserInfo(user);

            if (response != null)
            {
                if (response.Status.Equals(Common.STATUS_SUCCESS))
                {
                    this.edtName.Text = response.Data.Name;
                    this.edtPhone.Text = response.Data.Phone;
                    this.edtEmail.Text = response.Data.Email;
                    this.edtAddress.Text = response.Data.Address;

                    await Navigation.PopPopupAsync();
                }
                else
                {
                    await Navigation.PopPopupAsync();
                }
            }
            else
            {
                await Navigation.PopPopupAsync();
            }
        }
    }
}