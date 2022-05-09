using Microsoft.AspNetCore.Mvc;
using mtg_app.Models;
using mtg_lib.Library.Services;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;


namespace mtg_app.Controllers
{

    public class ShoppingController : Controller
    {
        
        CardService serviceCard = new CardService();
        //REMEMBER TO UPDATE THE DB NAME DEPENDING ON WHICH ONE YOURE USING.
        static string connection = "Server=LAPTOP-ERIC\\SQLEXPRESS;Database=Test;Trusted_Connection=True";

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AddItem()
        {
            //Set value in Session object.
            //HttpContext.Session.SetString("Usuario", "Eric");
            return View();
        }



        [HttpPost]  
        public ActionResult AddItem(CardViewModel cardView, int qty)
        {

            using (SqlConnection cn = new SqlConnection(connection))
            {
                cn.Open();
                string sql =  "INSERT INTO CartItems (UserId, ProductId, ProductName, ProductImageURL, Price, Qty, TotalPrice) VALUES(@param1,@param2,@param3,@param4,@param5,@param6,@param7)";
                using(SqlCommand cmd = new SqlCommand(sql,cn)) 
            {
                  cmd.Parameters.Add("@param1", SqlDbType.Int).Value = 1;  
                  cmd.Parameters.Add("@param2", SqlDbType.Int).Value = cardView.Multiverse_id;
                  cmd.Parameters.Add("@param3", SqlDbType.VarChar, 255).Value = cardView.Name;
                  cmd.Parameters.Add("@param4", SqlDbType.VarChar, 255).Value = cardView.Url;
                  cmd.Parameters.Add("@param5", SqlDbType.Float).Value = cardView.Price;
                  cmd.Parameters.Add("@param6", SqlDbType.Float).Value = qty;
                  cmd.Parameters.Add("@param7", SqlDbType.Float).Value = cardView.Price * cardView.Qty;
                

                  cmd.CommandType = CommandType.Text;
                  cmd.ExecuteNonQuery(); 
            }
            
            return View(Index());
        }

        /*

        public Task<CartItem> DeleteItem(int id)
        {
            return null;
        }
        public Task<CartItem> GetItem(int id)
        {
            return null;
        }
        public Task<IEnumerable<CartItem>> GetItems(ShoppingAddViewModel shoppingAddViewModel)
        {
            return null;
        }

        */
        
        }
    }
}