using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TiuShop.Model
{
    public class Product
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

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("categoryID")]
        public string CategoryId { get; set; }

        [JsonProperty("collectionID")]
        public string CollectionId { get; set; }

        [JsonProperty("stock")]
        public string Stock { get; set; }

        [JsonProperty("isWishList")]
        public bool IsWishList { get; set; }
    }
}
