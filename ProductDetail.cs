using App2.ViewModels;
using App2.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace App2.Model
{
    public class ProductDetail
    {

        public string Name { get; set; }
        public string ProductDetails { get; set; }
        public string Price { get; set; }
        public string Price1 { get; set; }
        public string ImageUrl { get; set; }
        public byte[] ImageData { get; set; }
        public string Quantity {get; set; }
        public string QuantityOld { get; set;}
        public string QuantityNew { get; set;}
    }
}
