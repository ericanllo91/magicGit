using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mtg_app.Models;
using mtg_app.Controllers;
using mtg_lib.Library.Services;
using Microsoft.AspNetCore.Mvc;
using mtg_app.Models;
using System.Net;
using Newtonsoft.Json;



namespace mtg_app.Controllers
{
    public class StoreController : Controller
    {

        CardService cardService = new CardService();
        public ActionResult Index()
        {
            var Raritys = new List<RarityViewModel>
            {
                new RarityViewModel{Name="ONE"},
                new RarityViewModel{Name="TWO"},
                new RarityViewModel{Name="THREE"},
                new RarityViewModel{Name="FOUR"}
            };
            return View(Raritys);
        }

        public string GetPrice(int? id)
        {
            // https://mpapi.tcgplayer.com/v2/product/130550/pricepoints
            // 129535
            WebClient wc = new WebClient();
            string start = wc.DownloadString($"https://mpapi.tcgplayer.com/v2/product/{id}/pricepoints");
            dynamic dobj = JsonConvert.DeserializeObject<dynamic>(start);
            string price = dobj[1]["marketPrice"];
            return price;
        }

/*
        public IActionResult BrowseR(string rarity)
         // MULTIVERSE
         // /store/browseM
        {
            CardsViewModel card = createRarity("U");
            return View(card);
        }
        */


        public IActionResult BrowseR(string rarity)
         // store/browser?rarity=U
         //U C M R S B
        {
            return View(createRarity(rarity));
        }



        public CardsViewModel createRarity(string rarity)
        {
            return new CardsViewModel
            {
                PageTitle = "Cards",
                Rarity = rarity,
                ColumnTitleProductName = "Product name",
                ColumnTitleUnitPrice = "Product price",
                Cards = cardService
                    .getCardByRarity(rarity)
                    .Take(10)
                    .Select(c =>
                        new CardViewModel
                        {
                            Name = c.Name,
                            Multiverse_id = c.MultiverseId,
                            //Rarity = c.RarityCode,
                            Price = GetPrice(c.MultiverseId),
                            Url = c.OriginalImageUrl
                        })
                    .ToList()
            };
        }


        public IActionResult BrowseM()
         // MULTIVERSE
         // /store/browseM
        {
            CardViewModel card = createMultiverse(1);
            return View(card);
        }
    

        [HttpPost]
        public IActionResult BrowseM(int multiverse)
         // MULTIVERSE
         // /store/browseM?multiverse=130550
        {
            return View(createMultiverse(multiverse));
        }

        public CardViewModel createMultiverse(int multiverse)
        {
            return new CardViewModel
                        {
                            Name = cardService.GetCardById(multiverse).Name,
                            Multiverse_id = cardService.GetCardById(multiverse).MultiverseId,
                            //Rarity = c.RarityCode,
                            //Price = GetPrice(c.MultiverseId),
                            Url = cardService.GetCardById(multiverse).OriginalImageUrl
                        };
            //return View(Cards);
        }

        public ActionResult Details()
        {
 
            return View();

        }
    }
}