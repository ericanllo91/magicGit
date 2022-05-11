using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mtg_app.Models;
using mtg_lib.Library.Services;
using Microsoft.AspNetCore.Mvc;
using mtg_app.Models;
using mtg_lib.Library.Services;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace mtg_app.Controllers
{
    public class UserController : Controller
    {
        CardService cardService = new CardService();


        public CardsViewModel createCards()
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
                            Url = c.OriginalImageUrl
                        })
                    .Take(1000)
                    .ToList()
                    
            };    
        }

        

        public IActionResult Login()
        {
            
            return View(createCards());
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
                            Url = c.OriginalImageUrl
                        })
                    .Take(100)
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
                            Url = cardService.GetCardById(multiverse).OriginalImageUrl
                        };
        }



        public IActionResult BrowseP()
         // store/browser?rarity=U
         //U C M R S B
        {
            return View(createPrice());
        }


        public CardsViewModel createPrice()
        {
            //double price = double.Parse("25.40");

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
                            Url = c.OriginalImageUrl
                        })    
                    .Take(1000)
                    .OrderBy(
                        cvm => cvm.Multiverse_id
                    )
                    .ToList()
                    
                    
            };
            
        }

        public ActionResult Details()
        {
 
            return View();

        }

        
    
    }
}