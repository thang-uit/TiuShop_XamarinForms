using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TiuShop.Model
{
    public class Order
    {
        [JsonProperty("orderID")]
        public string OrderId { get; set; }

        [JsonProperty("userID")]
        public string UserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("payment")]
        public string Payment { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("dateSuccess")]
        public string DateSuccess { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("totalPrice")]
        public int TotalPrice { get; set; }

        [JsonProperty("product")]
        public List<Cart> Product{ get; set; }
    }
}
