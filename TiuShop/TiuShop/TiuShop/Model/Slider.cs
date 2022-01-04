using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TiuShop.API;

namespace TiuShop.Model
{
    public class Slider
    {
        [JsonProperty("sliderID")]
        public string SliderId { get; set; }

        [JsonProperty("sliderImg")]
        public string SliderImg { get; set; }

        [JsonProperty("productID")]
        public string ProductId { get; set; }
    }
}
