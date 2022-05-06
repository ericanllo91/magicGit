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

    private CardsViewModel CreateTen()
    {
        return new CardsViewModel
        {
            Cards = cardService
                .TenCards()
                .Select(c =>
                    new CardViewModel
                    {
                        Name = c.Name,
                        Multiverse_id = c.MultiverseId,
                        Type = GetPrice(c.MultiverseId),                       
                        Url = c.OriginalImageUrl
                    })
                .ToList()        
        }; 
    }

}
