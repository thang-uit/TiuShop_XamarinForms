using System;
using System.Collections.Generic;
using System.Text;

namespace TiuShop.Model
{
    public class Order
    {
        public string orderID;
        public string orderName;
        public string orderEmail;
        public string orderPhone;
        public string address;
        public string orderDate;
        public List<OrderDetail> orderDetails;
        public int orderTotal;
    }
}
