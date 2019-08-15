using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using ProjectMvc.Models;

namespace ProjectMvc.Controllers
{

    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        [Route("Cart/AddToCart/{id}")]
        public ActionResult AddToCart(int id)
        {
            List<item> cart;
            if (Session["cart"]==null)
            {
                cart = new List<item>();
                Product prd = db.Product.Find(id) ;
                
                cart.Add(new item { ProductItem = prd, Quantity = 1 });

                Session["cart"] = cart;

            }

            else
            {
                cart = (List<item>)Session["cart"] ;
                int index = isExit(id);

                if(index !=-1)
                {
                    cart[index].Quantity++;
                }

                else
                {
                    cart.Add(new item { ProductItem = db.Product.Find(id), Quantity = 1 });
                }

                Session["cart"] = cart;
            }

            
           // return View("Index",cart);
            return RedirectToAction("Index","Home");
        }
        private int isExit(int prdId)
        {
            List<item> cart = (List<item>)Session["cart"];

            for (int i = 0; i < cart.Count; i++)
            {
                if(cart[i].ProductItem.ProductId.Equals(prdId))
                {
                    return i;
                }

            }
            return -1;

        }

        public ActionResult Remove(int id)
        {
            List<item> cart = (List<item>)Session["cart"];

            int index = isExit(id);

            cart.RemoveAt(index);
            Session["cart"] = cart;

            return RedirectToAction("Index","Home");

        }




        //public ActionResult Checkout()
        //{ // List<item> cart = (List<item>)Session["cart"];
            


        //    return RedirectToAction("Checkout");

        //}

    }
}