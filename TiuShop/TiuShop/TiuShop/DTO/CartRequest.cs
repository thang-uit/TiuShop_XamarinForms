using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TiuShop.DTO
{
    public class CartRequest
    {
        [JsonProperty("userID")]
        public string UserID { get; set; }

        [JsonProperty("productID")]
        public string ProductID { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("quantity")]
        public string Quantity { get; set; }
    }
}
