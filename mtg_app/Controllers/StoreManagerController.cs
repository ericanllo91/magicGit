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