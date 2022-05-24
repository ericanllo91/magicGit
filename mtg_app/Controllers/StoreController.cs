using Microsoft.AspNetCore.Mvc;
using mtg_app.Models;
using mtg_lib.Library.Services;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;




namespace mtg_app.Controllers
{
    public class StoreController : Controller
    {

        CardService cardService = new CardService();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        // IN THE SERVICE
        public double getPrice(int? id)
        {
            // https://mpapi.tcgplayer.com/v2/product/130550/pricepoints
            // 130550
            WebClient wc = new WebClient();
            string start = wc.DownloadString($"https://mpapi.tcgplayer.com/v2/product/{id}/pricepoints");
            dynamic dobj = JsonConvert.DeserializeObject<dynamic>(start);
            string price = dobj[0]["marketPrice"];
            if(price == null)
            {
                return 0;
            } else{
                double priceDouble = double.Parse(price);
                return priceDouble;
            }
            
        }

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
                    
                    .Select(c =>
                        new CardViewModel
                        {
                            Name = c.Name,
                            Multiverse_id = c.MultiverseId,
                            Price = getPrice(c.MultiverseId) ,
                            Url = c.OriginalImageUrl
                        })
                    .Where(cvm => 
                    {
                        return cvm.Price > 0;
                    })
                    .Take(10)
                    .OrderBy(p=> p.Price)
                    .ToList()
            };
        }


        // BROWSE BY MULTIVERSE !!
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
                            Price = getPrice(cardService.GetCardById(multiverse).MultiverseId),
                            Url = cardService.GetCardById(multiverse).OriginalImageUrl
                        };
            //return View(Cards);
        }



        public IActionResult BrowseP()
         // store/browser?rarity=U
         //U C M R S B
        {
            return View(createPrice());
        }



        public CardsViewModel createPrice()
        {
            return new CardsViewModel
            {
                PageTitle = "Cards",
                ColumnTitleProductName = "Product name",
                ColumnTitleUnitPrice = "Product price",
                Cards = cardService
                    .AllCards()
                    .Select(c => 

                        new CardViewModel
                        {
                            Name = c.Name,
                            Multiverse_id = c.MultiverseId,
                            Price = 25, 
                            //Price = getPrice(c.MultiverseId),
                            Url = c.OriginalImageUrl
                        })    
                    .Where(cvm => 
                    {
                        return cvm.Price > 0;
                    })
                    .OrderBy(cvm => 
                        cvm.Price
                    )
                    .Take(10)
                    .ToList()
            };
            
        }

        public ActionResult Details()
        {
 
            return View();

        }

        
    }
}