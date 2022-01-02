using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TiuShop.Model
{
    public partial class Account
    {
        [JsonProperty("userID")]
        public int UserId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("role")]
        public int Role { get; set; }
    }
}
