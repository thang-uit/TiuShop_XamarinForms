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
    }
}
