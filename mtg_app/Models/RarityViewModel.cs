using System;
using System.ComponentModel.DataAnnotations;
using mtg_app.Models;

namespace mtg_app.Models

{
    public class RarityViewModel {

        public List<CardViewModel> Cards {get;set;}
        public string Name {get;set;}
        public string Code {get;set;}
        public string PageTitle { get; set; }
        public string ColumnTitleUnitPrice { get; set; }
        public string ColumnTitleProductName { get; set; }
        public int CategoryId {get; set;}
        public string SearchValue {get; set;}
        
    }
}