using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TiuShop.DTO
{
    public class CommentRequest
    {
        [JsonProperty("userID")]
        public string UserID { get; set; }

        [JsonProperty("productID")]
        public string ProductID { get; set; }

        [JsonProperty("rating")]
        public string Rating { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
