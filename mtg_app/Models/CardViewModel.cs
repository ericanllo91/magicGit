using System;
using System.ComponentModel.DataAnnotations;
namespace mtg_app.Models
{
    public class CardViewModel {
        public string Name {get; set;}

        public RarityViewModel Rarity {get;set;}
        public string Url {get; set;}

        public string Price {get;set;}
        public long CategoryId {get;set;}
        public int? Multiverse_id {get;set;}
        public string Type {get;set;}
    }
}