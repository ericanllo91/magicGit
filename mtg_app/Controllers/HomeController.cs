using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mtg_app.Models;
using Newtonsoft.Json;
using mtg_lib.Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace mtg_app.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    CardService serviceCard = new CardService();



    public CardsViewModel createCards()
        {
            return new CardsViewModel
            {
                PageTitle = "Cards",
                ColumnTitleProductName = "Product name",
                ColumnTitleUnitPrice = "Product price",
                Cards = serviceCard
                    .AllCards()
                    .Select(c => 

                        new CardViewModel
                        {
                            Name = c.Name,
                            Multiverse_id = c.MultiverseId,
                            Url = c.OriginalImageUrl
                        })
                    .Take(10)
                    .ToList()
                    
            };    
        }

    public IActionResult Index()
    {
        //Read session
        HomeModel testlistFromSession = new HomeModel();
        string serializedTestStringFromSession = HttpContext.Session.GetString("user");
        if(serializedTestStringFromSession != null){
            testlistFromSession.Session = JsonConvert.DeserializeObject<List<String>>(serializedTestStringFromSession);
            testlistFromSession.ShowContent = true;
        }
        return View(testlistFromSession);
        //return View(createCards());
    }

    public IActionResult Privacy()
    {
        //Create the session
        List<String> testlist = new List<string>();
        testlist.Add("Your session has started, enjoy the website");

        string serializedTestString = JsonConvert.SerializeObject(testlist);
        HttpContext.Session.SetString("user",serializedTestString);
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
