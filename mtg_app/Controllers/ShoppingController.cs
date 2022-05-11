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
        static string connection = "Server=LAPTOP-ERIC\\SQLEXPRESS;Database=Test;Trusted_Connection=True";
        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddItem()
         // store/browser?rarity=U
         //U C M R S B
        {
            return View();
        }

        public IActionResult Cart()
         // store/browser?rarity=U
         //U C M R S B
        {
            return View(createBasket());
        }

        public CartsItemViewModel createBasket()
        {
            return new CartsItemViewModel
            {
                PageTitle = "Cart",
                Carts = serviceShopping
                    .getAllItems()
                    .Take(10)
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
        
         /*
        public CardsViewModel createBasket()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {

                //retrieve the SQL Server instance version
                string sql = "SELECT * from CartItems";
                //define the SqlCommand object
                SqlCommand cmd = new SqlCommand(sql, conn);

                //open connection
                conn.Open();

                //execute the SQLCommand
                SqlDataReader dr = cmd.ExecuteReader();

                //check if there are records
                if (dr.HasRows)
                {
                    List<CardViewModel> listOfCards = new List<CardViewModel>();
                    while (dr.Read())
                    {
                        //double pp = Convert.ToDouble(dr.GetFloat(4));
                        CardViewModel card = new CardViewModel 
                        {
                            Multiverse_id = dr.GetInt32(1),
                            Name = dr.GetString(2),
                            Url = dr.GetString(3),
                            //Price = pp,
                            //Qty = dr.GetFloat(6),
                            //TotalPrice = dr.GetFloat(7)
                        };
                        
                        listOfCards.Add(card);
                    }
                    

                    IEnumerable<CardViewModel> enumerable = listOfCards;
                    return new CardsViewModel
                        {
                            PageTitle = "Cards",
                            ColumnTitleProductName = "Product name",
                            ColumnTitleUnitPrice = "Product price",
                            Cards = listOfCards
                                .Select(c =>
                                    new CardViewModel
                                {
                                    Multiverse_id = c.Multiverse_id,
                                    Name = c.Name,
                                    Url = c.Url,
                                    //Price = c.Price,
                                    //Qty = c.Qty,
                                    //TotalPrice = c.TotalPrice
                                }).ToList()
                        };
                    }

                }
                return null;
               
        
            }
        */
        [HttpPost]  
        public ActionResult AddItem(int userId, int productId, string productName, string productImageUrl, double price, double totalPrice, double qty)
        {
        //int Userid, int ProductId, string Productname, string productimageurl, double price, double totalprice, double qty

            serviceShopping.addItem(userId,productId, productName, productImageUrl, price, totalPrice, qty);
            return View(AddItem());


        }

        [HttpPost] 
        public ActionResult deleteItem(int userId, int productId, string productName, string productImageUrl, double price, double totalPrice, double qty)
        {
        //int Userid, int ProductId, string Productname, string productimageurl, double price, double totalprice, double qty

            serviceShopping.deleteItem(userId,productId, productName, productImageUrl, price, totalPrice, qty);
            return View("AddItem");


        }


    }
}