using System;
using System.Linq;
using System.Collections.Generic;
using mtg_lib.Library.Models;
using System.Net;
using Newtonsoft.Json;

namespace mtg_lib.Library.Services

{

    public class ShoppingService 
    {

        //ShoppingContext context = new ShoppingContext();
        //CardService contextTwo = new CardService();
        magicContext context = new magicContext();

        public IEnumerable<Cartitem> getAllItems()
        {
            return context.Cartitems.Where(c => c.Productimageurl != null);
        }

        public Cartitem addItem(int userId, int productId, string productname, string productImageUrl, double? price, double? totalprice, int? qty)
        {
            Cartitem cartitem = new Cartitem();
            cartitem.Userid = userId;
            cartitem.Productid = productId;
            cartitem.Productname = productname;
            cartitem.Productimageurl = productImageUrl;
            cartitem.Price = price;
            cartitem.Totalprice = totalprice;
            cartitem.Qty = qty;

            context.Add(cartitem);
            context.SaveChanges();

            return cartitem;
        }

        public Cartitem deleteItem(int userId, int productId, string productname, string productImageUrl, double? price, double? totalprice, int? qty)
        {
            Cartitem cartitem = new Cartitem();
            cartitem.Userid = userId;
            cartitem.Productid = productId;
            cartitem.Productname = productname;
            cartitem.Productimageurl = productImageUrl;
            cartitem.Price = price;
            cartitem.Totalprice = totalprice;
            cartitem.Qty = qty;

            context.Remove(cartitem);
            context.SaveChanges();

            return cartitem;
        }

        public Cartitem updateTotal()
        {
            Cartitem cartitem = new Cartitem();
            //context.Cartitems.Where(c => c.Qty > 1)

            //context.Set.CartItems.Where(c)

            context.Update(cartitem);
            context.SaveChanges();

            return cartitem;
        }

        public Cartitem GetCart(int productId){
            var results = context.Cartitems.Where(c => c.Productid == productId);
            if(results.Count() == 0){
                return null;
            }
            return results.First();
            
        }

    


        public Cartitem Update(int id, int qty){
            Cartitem cart = GetCart(id);
            context.SaveChanges();

            return cart;
        }









    }

        
        
}