using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TiuShop.Model
{
    public class Category
    {
        [JsonProperty("categoryID")]
        public string CategoryID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
