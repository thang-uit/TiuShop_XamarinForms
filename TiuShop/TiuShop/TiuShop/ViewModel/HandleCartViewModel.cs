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
    public class HandleCartViewModel : BaseViewModel
    {
        private bool isCartVisible = false;
        public bool IsCartVisible { get => isCartVisible; set => SetProperty(ref isCartVisible, value); }

        private bool isCartDisable = false;
        public bool IsCartDisable { get => isCartDisable; set => SetProperty(ref isCartDisable, value); }

        private decimal totalPrice = 0;
        public decimal TotalPrice { get => totalPrice; set => SetProperty(ref totalPrice, value); }

        public ObservableCollection<Cart> Items { get; set; }

        public Command loadItemCommand { get; }

        public Command itemDeleteCommand { get; }

        public Command itemIncreaseCommand { get; }

        public Command itemDecreaseCommand { get; }

        public HandleCartViewModel()
        {
            Items = new ObservableCollection<Cart>();
            loadItemCommand = new Command(async () => await LoadItem());
            itemDeleteCommand = new Command<Cart>(DeleteItem);
            //itemIncreaseCommand = new Command<Cart>(IncreaseItem);
            //itemDecreaseCommand = new Command<Cart>();
        }

        public async Task LoadItem()
        {
            Items.Clear();
            IsBusy = true;

            try
            {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new MyLoading());

                var apiResponse = RestService.For<IApi>(Common.url);
                CartRequest cart = new CartRequest() { UserID = Preferences.Get(Common.KEY_USERID, "") };
                var response = await apiResponse.GetCart(cart);

                if (response != null)
                {
                    if (response.Status.Equals(Common.STATUS_SUCCESS))
                    {
                        foreach (var data in response.Data)
                        {
                            data.Image[0] = Common.imgUrl + data.Image[0];
                            data.FinalPrice = data.FinalPrice.Replace(".", "");
                            TotalPrice = TotalPrice + (Convert.ToDecimal(data.Quantity) * Convert.ToDecimal(data.FinalPrice));
                            Items.Add(data);
                        }
                        if (Items.Count > 0)
                        {
                            this.IsCartVisible = true;
                            this.IsCartDisable = false;
                        }
                        else
                        {
                            this.IsCartVisible = false;
                            this.IsCartDisable = true;
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
            if (cart == null)
            {
                return;
            }
            var option = await Application.Current.MainPage.DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent15"].ToString(), App.Current.Resources["lblAlertOK"].ToString(), App.Current.Resources["lblAlertCancel"].ToString());

            if (option)
            {
                try
                {
                    var apiResponse = RestService.For<IApi>(Common.url);
                    CartRequest cartRequest = new CartRequest() { CartID = cart.CartID };
                    var response = await apiResponse.DeleteCart(cartRequest);

                    if (response != null)
                    {
                        if (response.Status.Equals(Common.STATUS_SUCCESS))
                        {
                            Items.Remove(cart);

                            if (Items.Count > 0)
                            {
                                this.IsCartVisible = true;
                                this.IsCartDisable = false;
                            }
                            else
                            {
                                this.IsCartVisible = false;
                                this.IsCartDisable = true;
                            }

                            caculateTotalPrice();
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

        //public async void IncreaseItem(Cart cart)
        //{
        //    if (cart == null)
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        var apiResponse = RestService.For<IApi>(Common.url);
        //        CartRequest cartRequest = new CartRequest() { CartID = cart.CartID, Quantity = cart.Quantity };
        //        var response = await apiResponse.UpdateCart(cartRequest);

        //        if (response != null)
        //        {
        //            if (response.Status.Equals(Common.STATUS_SUCCESS))
        //            {
        //                Items.Remove(cart);
        //                Items.Add(cart);

        //                caculateTotalPrice();
        //            }
        //            else
        //            {
        //                await Application.Current.MainPage.DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent0"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        await Application.Current.MainPage.DisplayAlert(App.Current.Resources["lblAlert"].ToString(), App.Current.Resources["lblAlertContent0"].ToString(), App.Current.Resources["lblAlertOK"].ToString());
        //    }
        //}

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private void caculateTotalPrice()
        {
            foreach (var price in Items)
            {
                price.FinalPrice = price.FinalPrice.Replace(".", "");
                TotalPrice = TotalPrice + Convert.ToDecimal(price.FinalPrice);
            }
        }
    }
}
