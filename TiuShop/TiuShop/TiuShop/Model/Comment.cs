using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TiuShop.Model
{
    public class Comment
    {
        [JsonProperty("commentID")]
        public string CommentId { get; set; }

        [JsonProperty("productID")]
        public string ProductId { get; set; }

        [JsonProperty("userID")]
        public string UserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rating")]
        public string Rating { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
