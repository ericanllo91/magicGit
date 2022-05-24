using System;
using System.Linq;
using System.Collections.Generic;
using mtg_lib.Library.Models;
using System.Net;
using Newtonsoft.Json;

namespace mtg_lib.Library.Services

{

    public class CardService {

        magicContext context = new magicContext();

        public IEnumerable<Card> AllCards()
        {
            return context.Cards.Where(c => c.OriginalImageUrl != null);
        }

        
        public IEnumerable<Card> TenCards(int searchString)
        {
            return context.Cards.Where(c => c.MultiverseId.Equals(searchString));
        }
        

        public List<Card> getCardByRarity(string rarity) {
            var results = context.Cards.Where(c => c.RarityCode == rarity && c.OriginalImageUrl != null);
            if(results.Count() == 0){
                return new List<Card>();
            }
            
            return results.ToList();

        }

     

        public Card GetCardById(int id)
        {
            var results = context.Cards.Where(c => c.MultiverseId == id);
            if (results.Count() == 0)
            {
                return null;
            }

            return results.First();
        }


        public Card Add(string name, string rarity){
            Card card = new Card();

            card.Name = name;
            card.RarityCode = rarity;
            
            context.Add(card);
            context.SaveChanges();

            return card;
        }

        public Card Add(string name, string? rarity, string convertedManaCost, string type, string setCode, string number, string layout, string image, string mtgId){
            Card card = new Card();
            CardColor cc = new CardColor();
            Set setcode = new Set();

            setcode.Code = setCode;
            setcode.Name = name;


            card.Name = name;
            card.RarityCode = rarity;
            card.ConvertedManaCost = convertedManaCost;
            card.Type = type;
            card.SetCode = setCode;
            card.Number = number;
            card.Layout = layout;
            card.Image = image;
            card.MtgId = mtgId;

            context.Add(setcode);
            context.Add(card);
            context.SaveChanges();

            return card;
        }
    }
    
    }