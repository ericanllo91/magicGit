using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mtg_app.Models;


namespace mtg_app.Controllers
{
    public class StoreController : Controller
    {
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


        public ActionResult Browse(string category)
        // store/browse?category=perro
        {
            var Rarity = new RarityViewModel{
                Name = "rarity "+ category
            };
            return View(Rarity);
        }

        public ActionResult Details(int id)
        {
            var Card = new CardViewModel
            {Name = "name"+id};
            return View(Card);

        }
    }
}