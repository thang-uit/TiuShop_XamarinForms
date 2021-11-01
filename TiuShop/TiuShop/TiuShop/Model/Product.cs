using System;
using System.Collections.Generic;
using System.Text;

namespace TiuShop.Model
{
    public class Product
    {
        public string productID;
        public string productName;
        public List<ProductImage> productImages;
        public string productDescription;
        public string productDate;
        public int productPrice;
        public int productSale;
        public int productFinalPrice;
        public string productCategory;
        public string productGroupCategory; //New, Hot, Bestseller
        public string[] productColor;
        public string[] productSize;
        public List<Comment> comments;
        public string productStock;
    }
}
