using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mtg_app.Models;
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
           // 129535
            WebClient wc = new WebClient();
            string start = wc.DownloadString($"https://mpapi.tcgplayer.com/v2/product/{id}/pricepoints");
            dynamic dobj = JsonConvert.DeserializeObject<dynamic>(start);
            string price = dobj[1]["marketPrice"];
            //return View(CreateCardsViewModel());
            return price;
        }


        //[Route("[action]")]
        public IActionResult Browse(string rarity)
         // store/browse?rarity=U
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
            //return View(Cards);
        }

        public ActionResult Details(int id)
        {
            var Card = new CardViewModel
            {Name = "name"+id};
            return View(Card);

        }
    }
}