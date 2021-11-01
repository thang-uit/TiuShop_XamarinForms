using System;
using System.Collections.Generic;
using System.Text;

namespace TiuShop.Model
{
    public class User
    {
        public string userID;
        public string userToken;
        public string userName;
        public string userAvatar;
        public string userEmail;
        public string userPhone;
        public List<Address> addresses;
        public string userCreateDay;
    }
}
