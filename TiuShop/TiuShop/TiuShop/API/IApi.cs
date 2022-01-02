using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TiuShop.Model;

namespace TiuShop.API
{
    public interface IApi
    {
        //[Get("/abc.txt")]
        //Task<string> Text();

        [Post("/User/Login.php")]
        Task<Account> Login([AliasAs("username")] string username, [AliasAs("password")] string password);
    }
}
