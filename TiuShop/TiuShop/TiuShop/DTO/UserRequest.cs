using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TiuShop.DTO
{
    public class UserRequest
    {
        [JsonProperty("userID")]
        public string UserID { get; set; }
    }
}
