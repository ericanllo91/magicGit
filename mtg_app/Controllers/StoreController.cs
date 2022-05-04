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
        public IActionResult Index()
        {
            return View();
        }


        public string Browse(string category)
        // store/browse?category=perro
        {
            string message = "hello "+ category;
            return message;
        }

        public ActionResult Details(int id)
        {
            var Item = new CardViewModel
            {Name = "name"+id};
            return View(Item);

        }
    }
}