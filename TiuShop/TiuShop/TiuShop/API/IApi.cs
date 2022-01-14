using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TiuShop.DTO;
using TiuShop.Model;

namespace TiuShop.API
{
    public interface IApi
    {
        [Post("/User/Login.php")]
        Task<CustomApiResponse<Account>> Login([Body] AccountRequest account);

        [Post("/User/Register.php")]
        Task<CustomApiResponse<Account>> Register([Body] AccountRequest account);

        [Post("/User/ChangePassword.php")]
        Task<CustomApiResponse<Account>> ChangePassword([Body] AccountRequest account);

        [Post("/User/GetUserInfo.php")]
        Task<CustomApiResponse<User>> GetUserInfo([Body] UserRequest user);

        [Post("/User/UpdateUserInfo.php")]
        Task<CustomApiResponse<User>> UpdateUserInfo([Body] UserRequest user);

        [Get("/Product/GetSlider.php?amount={amount}")]
        Task<CustomApiResponse<List<Slider>>> GetSlider([AliasAs("amount")] int amount);

        [Get("/Product/GetGroupProduct.php?amount={amount}&option={option}")]
        Task<CustomApiResponse<List<Product>>> GetGroupProduct([AliasAs("option")] string option, [AliasAs("amount")] int amount);

        [Get("/Product/GetCategoryProduct.php?categoryID={categoryID}")]
        Task<CustomApiResponse<List<Product>>> GetCategoryProduct([AliasAs("categoryID")] string categoryID);

        [Get("/Product/GetCollectionsProduct.php?collectionsID={collectionsID}")]
        Task<CustomApiResponse<List<Product>>> GetCollectionsProduct([AliasAs("collectionsID")] string collectionsID);

        [Get("/Product/SearchProduct.php?keyword={keyword}")]
        Task<CustomApiResponse<List<Product>>> SearchProduct([AliasAs("keyword")] string keyword);

        [Get("/Category/GetCategory.php")]
        Task<CustomApiResponse<List<Category>>> GetCategory();

        [Get("/Collections/GetCollections.php")]
        Task<CustomApiResponse<List<Collections>>> GetCollections();

        [Post("/Product/GetProductDetail.php")]
        Task<CustomApiResponse<Product>> GetProductDetail([Body] CartRequest cart);

        [Post("/Cart/GetWishList.php")]
        Task<CustomApiResponse<List<Cart>>> GetWishList([Body] CartRequest wishList);

        [Post("/Cart/HandleWishList.php")]
        Task<CustomApiResponse<Cart>> HandleWishList([Body] CartRequest wishList);

        [Post("/Cart/MoveToCart.php")]
        Task<CustomApiResponse<Cart>> MoveToCart([Body] CartRequest cart);

        [Post("/Cart/GetCart.php")]
        Task<CustomApiResponse<List<Cart>>> GetCart([Body] CartRequest cart);

        [Post("/Cart/AddToCart.php")]
        Task<CustomApiResponse<Cart>> AddToCart([Body] CartRequest cart);

        [Post("/Cart/DeleteCart.php")]
        Task<CustomApiResponse<Cart>> DeleteCart([Body] CartRequest cart);

        [Post("/Cart/UpdateCart.php")]
        Task<CustomApiResponse<Cart>> UpdateCart([Body] CartRequest cart);

        [Post("/Order/AddNewOrder.php")]
        Task<CustomApiResponse<Order>> AddNewOrder([Body] OrderRequest order);

        [Post("/Order/GetAmountOrder.php")]
        Task<CustomApiResponse<AmountOrder>> GetAmountOrder([Body] UserRequest user);

        [Post("/Order/GetOrderInfo.php")]
        Task<CustomApiResponse<List<Order>>> GetOrderInfo([Body] UserRequest user);

        [Post("/Order/UpdateOrderStatus.php")]
        Task<CustomApiResponse<Order>> UpdateOrderStatus([Body] OrderRequest order);

        [Get("/Comment/GetComment.php?productID={productID}")]
        Task<CustomApiResponse<List<Comment>>> GetComment([AliasAs("productID")] string productID);
    }
}
