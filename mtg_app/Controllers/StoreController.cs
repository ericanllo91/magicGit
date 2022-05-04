using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


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

        public string Details(int id)
        {
            return "Hello from Details" + id;
        }
    }
}