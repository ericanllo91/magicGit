using System;
using System.ComponentModel.DataAnnotations;
namespace mtg_app.Models
{
    public class CardViewModel {
        public string Name {get; set;}

        public string? Rarity {get;set;}
        public string? Url {get; set;}

        public string? Price {get;set;}
        public long? CategoryId {get;set;}

        public int? Multiverse_id {get;set;}
        //public string? Type {get;set;}

        public string? Code {get;set;}

        //NEW PART
        public string ConvertedManaCost { get; set; }
        public string Type { get; set; }
        public string SetCode { get; set; }
        public string Number { get; set; }
        public string Layout { get; set; }
        public string Image { get; set; }
        public string MtgId { get; set; }
        //public virtual Set SetCodeNavigation { get; set; }
    }
}