using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mtg_app.Models;
using System.Net;
using Newtonsoft.Json;
using mtg_app.Controllers;
using mtg_lib.Library.Services;

namespace mtg_app.Controllers
{
    public class StoreManagerController : Controller
    {
        CardService cardService = new CardService();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveCard(CardViewModel card)
        {

            var errors = ModelState.Where(x => x.Value.Errors.Any())
                .Select(x => new { x.Key, x.Value.Errors });
            
            System.Diagnostics.Debug.WriteLine(errors);
            // ModelState is always available in a controller.
            if (ModelState.IsValid)
            {
                cardService.Add((string)card.Name, (string?)card.Rarity, (string)card.ConvertedManaCost, (string)card.Type, (string)card.SetCode, (string)card.Number, (string)card.Layout, (string)card.Image, (string)card.MtgId);

                return RedirectToAction("Browse",
                    "Store",
                    new { Rarity = card.Rarity });
            }
            

            ModelState.AddModelError("", "Not all fields are filled in correctly.");
            return View("Index");
        }





        //[Route("[action]")]
        public IActionResult Create(string rarity)
        //storemanager/create?rarity=U
        {
            return View(createCard(rarity));
        }

        public CardViewModel createCard(string rarity)
        {
             return new CardViewModel
            {
                Rarity = rarity
    
            };

        }






        
    }
}

// var result = new ControllerB().FileUploadMsgView("some string");