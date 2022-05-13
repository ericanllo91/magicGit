using System;
using System.Collections.Generic;

namespace mtg_lib.Library.Models
{
    public partial class Cartitem
    {
        public int Userid { get; set; }
        public int Productid { get; set; }
        public string? Productname { get; set; }
        public string? Productimageurl { get; set; }
        public double? Price { get; set; }
        public double? Totalprice { get; set; }
        public int? Qty { get; set; }

        public virtual Magicuser User { get; set; } = null!;
    }
}
