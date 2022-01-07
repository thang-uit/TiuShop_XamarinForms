using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TiuShop.Model
{
    public class Cart
    {
        [JsonProperty("productID")]
        public string ProductId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public List<string> Image { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("sale")]
        public string Sale { get; set; }

        [JsonProperty("isSale")]
        public string IsSale { get; set; }

        [JsonProperty("finalPrice")]
        public string FinalPrice { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("quantity")]
        public string Quantity { get; set; }
    }
}
