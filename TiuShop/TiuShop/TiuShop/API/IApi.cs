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
        //[Get("/abc.txt")]
        //Task<string> Text();

        [Post("/User/Login.php")]
        Task<CustomApiResponse<Account>> Login([Body] AccountRequest account);

        [Post("/User/Register.php")]
        Task<CustomApiResponse<Account>> Register([Body] AccountRequest account);

        [Get("/Product/GetSlider.php?amount={amount}")]
        Task<CustomApiResponse<List<Slider>>> GetSlider([AliasAs("amount")] int amount);

        [Get("/Product/GetGroupProduct.php?amount={amount}&option={option}")]
        Task<CustomApiResponse<List<Product>>> GetGroupProduct([AliasAs("option")] string option, [AliasAs("amount")] int amount);

        [Get("/Product/GetCategoryProduct.php?categoryID={categoryID}")]
        Task<CustomApiResponse<List<Product>>> GetCategoryProduct([AliasAs("categoryID")] string categoryID);

        [Get("/Product/GetCollectionsProduct.php?collectionsID={collectionsID}")]
        Task<CustomApiResponse<List<Product>>> GetCollectionsProduct([AliasAs("collectionsID")] int collectionsID);

        [Get("/Product/SearchProduct.php?keyword={keyword}")]
        Task<CustomApiResponse<List<Product>>> SearchProduct([AliasAs("keyword")] string keyword);

        [Get("/Category/GetCategory.php")]
        Task<CustomApiResponse<List<Category>>> GetCategory();

        [Get("/Collections/GetCollections.php")]
        Task<CustomApiResponse<List<Collections>>> GetCollections();

        [Get("/Product/GetProductDetail.php?productID={productID}")]
        Task<CustomApiResponse<Product>> GetProductDetail(string productID);
    }
}
