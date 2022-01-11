using Refit;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TiuShop.API;
using TiuShop.DTO;
using TiuShop.Model;
using TiuShop.View.Popup;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TiuShop.ViewModel
{
    public class HandleWishListViewModel : BaseViewModel
    {
        private bool isWishListVisible = false;
        public bool IsWishListVisible { get => isWishListVisible; set => SetProperty(ref isWishListVisible, value); }

        private bool isWishListDisable = false;
        public bool IsWishListDisable { get => isWishListDisable; set => SetProperty(ref isWishListDisable, value); }

        public ObservableCollection<Cart> Items { get; set; }

        public Command loadItemCommand { get; }

        public Command itemDeleteCommand { get; }

        public Command itemMoveToCartCommand { get; }

        public HandleWishListViewModel()
        {
            Items = new ObservableCollection<Cart>();
            loadItemCommand = new Command(async () => await LoadItem());
            itemDeleteCommand = new Command<Cart>(DeleteItem);
            itemMoveToCartCommand = new Command<Cart>(MoveToCart);
        }

        public async Task LoadItem()
        {
            Items.Clear();
            IsBusy = true;

            try
            {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new MyLoading());

                var apiResponse = RestService.For<IApi>(Common.url);
                CartRequest wishList = new CartRequest() { UserID = Preferences.Get(Common.KEY_USERID, "") };
                var response = await apiResponse.GetWishList(wishList);

                if (response != null)
                {
                    if (response.Status.Equals(Common.STATUS_SUCCESS))
                    {
                        foreach (var data in response.Data)
                        {
                            data.Image[0] = Common.imgUrl + data.Image[0];
                            Items.Add(data);
                        }

                        if(Items.Count > 0)
                        {
                            this.IsWishListVisible = true;
                            this.IsWishListDisable = false;
                        }
                        else
                        {
                            this.IsWishListVisible = false;
                            this.IsWishListDisable = true;
                        }
                        await Application.Current.MainPage.Navigation.PopPopupAsync();
                    }
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PopPopupAsync();
                }
            }
            catch (Exception)
            {
                await Application.Current.MainPage.Navigation.PopPopupAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void DeleteItem(Cart cart)
        {
            if(cart == null)
            {
                return;
            }
            var option = await Application.Current.MainPage.DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent15"].ToString(), App.Current.Resources["lblAlertOK"].ToString(), App.Current.Resources["lblAlertCancel"].ToString());

            if (option)
            {
                try
                {
                    var apiResponse = RestService.For<IApi>(Common.url);
                    CartRequest wishList = new CartRequest() { CartID = cart.CartID };
                    var response = await apiResponse.DeleteCart(wishList);

                    if (response != null)
                    {
                        if (response.Status.Equals(Common.STATUS_SUCCESS))
                        {
                            Items.Remove(cart);

                            if (Items.Count > 0)
                            {
                                this.IsWishListVisible = true;
                                this.IsWishListDisable = false;
                            }
                            else
                            {
                                this.IsWishListVisible = false;
                                this.IsWishListDisable = true;
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent0"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
                        }
                    }
                }
                catch
                {
                    await Application.Current.MainPage.DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent0"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
                }
            }
        }

        public async void MoveToCart(Cart cart)
        {
            if (cart == null)
            {
                return;
            }

            try
            {
                var apiResponse = RestService.For<IApi>(Common.url);
                CartRequest wishList = new CartRequest() { CartID = cart.CartID, UserID = Preferences.Get(Common.KEY_USERID, ""), ProductID = cart.ProductId, Size = cart.Size };
                var response = await apiResponse.MoveToCart(wishList);

                if (response != null)
                {
                    if (response.Status.Equals(Common.STATUS_SUCCESS))
                    {
                        Items.Remove(cart);

                        if (Items.Count > 0)
                        {
                            this.IsWishListVisible = true;
                            this.IsWishListDisable = false;
                        }
                        else
                        {
                            this.IsWishListVisible = false;
                            this.IsWishListDisable = true;
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent0"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
                        this.IsWishListVisible = false;
                        this.IsWishListDisable = true;
                    }
                }
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent0"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
                this.IsWishListVisible = false;
                this.IsWishListDisable = true;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
