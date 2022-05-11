using System;
using System.ComponentModel.DataAnnotations;
using mtg_app.Models;

namespace mtg_app.Models

{
    public class CartItemViewModel {

        public int Userid { get; set; }
        [Key]
        public int Productid { get; set; }
        public string? Productname { get; set; }
        public string? Productimageurl { get; set; }
        public double? Price { get; set; }
        public double? Totalprice { get; set; }
        public int? Qty { get; set; }
        
    }
}