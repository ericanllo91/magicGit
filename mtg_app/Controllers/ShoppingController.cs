using Microsoft.AspNetCore.Mvc;
using mtg_app.Models;
using mtg_app.Controllers;
using mtg_lib.Library.Services;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;



namespace mtg_app.Controllers
{

    public class ShoppingController : Controller
    {
        

        CardService serviceCard = new CardService();
        ShoppingService serviceShopping = new ShoppingService();
        //REMEMBER TO UPDATE THE DB NAME DEPENDING ON WHICH ONE YOURE USING.        

        public IActionResult Index()
        {
            return View(updateCarrito());
        }

        public IActionResult AddItem()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View(createBasket());
        }
        

        public CartsItemViewModel createBasket()
        {
            return new CartsItemViewModel
            {
                Number = 0,
                PageTitle = "Cart",
                Carts = serviceShopping
                    .getAllItems()
                    .Select(c =>
                        new CartItemViewModel
                        {
                            Userid = c.Userid,
                            Productid = c.Productid,
                            Productname = c.Productname,
                            Productimageurl = c.Productimageurl,
                            Price = c.Price,
                            Totalprice = c.Totalprice,
                            Qty = c.Qty
                        })   
                    .ToList()         
            };
        }

        public List<CartItemViewModel> updateCarrito()
        {
            List<double?> tots = new List<double?>();
            CartsItemViewModel carrito = createBasket();
            //list de carritos
            var car = carrito.Carts;
            car.ForEach(c => c.Price = (c.Price*c.Qty));
            car.ForEach(c => tots.Add(c.Price));
            car.ForEach(c => c.Totalprice = tots.Sum());
            return car;
        }


 
        

        [HttpPost]  
        public ActionResult AddItem(int userId, int productId, string productName, string productImageUrl, double price, double totalPrice, int qty)
        {
        //int Userid, int ProductId, string Productname, string productimageurl, double price, double totalprice, double qty

            serviceShopping.addItem(userId,productId, productName, productImageUrl, price, totalPrice, qty);
            return View(AddItem());
        }



        [HttpPost] 
        public ActionResult deleteItem(int userId, int productId, string productName, string productImageUrl, double price, double totalPrice, int qty)
        {
        //int Userid, int ProductId, string Productname, string productimageurl, double price, double totalprice, double qty

            serviceShopping.deleteItem(userId,productId, productName, productImageUrl, price, totalPrice, qty);
            return View("AddItem");


        }


    }
}