using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TiuShop.Model
{
    public class AmountOrder
    {
        [JsonProperty("order0")]
        public int Order0 { get; set; }

        [JsonProperty("order1")]
        public int Order1 { get; set; }

        [JsonProperty("order2")]
        public int Order2 { get; set; }

        [JsonProperty("order3")]
        public int Order3 { get; set; }

        [JsonProperty("order4")]
        public int Order4 { get; set; }
    }
}
