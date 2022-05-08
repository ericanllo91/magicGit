using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mtg_app.Models;
using mtg_lib.Library.Services;
using Microsoft.AspNetCore.Mvc;
using mtg_app.Models;
using System.Net;
using Newtonsoft.Json;

namespace mtg_app.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    CardService cardService = new CardService();

    public IActionResult Index()
    {
        return View(CreateTen());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private CardsViewModel CreateTen()
    {
        return new CardsViewModel
        {
            Cards = cardService
                .AllCards()
                .Take(5)
                .Select(c =>
                    new CardViewModel
                    {
                        Name = c.Name,
                        Multiverse_id = c.MultiverseId,
                        //Type = GetPrice(c.MultiverseId),                       
                        Url = c.OriginalImageUrl,
                        Price = new StoreController().GetPrice(c.MultiverseId)
                    })
                .ToList()        
        }; 
    }

}
