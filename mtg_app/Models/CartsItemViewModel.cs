using System;
using System.ComponentModel.DataAnnotations;
using mtg_lib.Library.Services;

namespace mtg_app.Models

{
    public class CartsItemViewModel {
    
        public List<CartItemViewModel> Carts {get; set;}
        public string PageTitle { get; set; }
        public string ColumnTitleUnitPrice { get; set; }
        public string ColumnTitleProductName { get; set; }
        public string SearchValue {get; set;}
        
    }
}