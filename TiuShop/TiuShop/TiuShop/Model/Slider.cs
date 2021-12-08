using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TiuShop.Model
{
    public partial class Slider
    {
        [JsonProperty("sliderID")]
        public int SliderId { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("songID")]
        public int SongId { get; set; }
    }
}
