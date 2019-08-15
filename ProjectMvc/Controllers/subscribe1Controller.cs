using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectMvc.Models;


namespace ProjectMvc.Controllers
{
    public class subscribe1Controller : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: subscribe1
        
        [HttpPost ]
        public ActionResult AddSubscribe(string email)
        {

            Sub newSub = new Sub()
            {
                SubscribEmail =email
            };
            context.Sub.Add(newSub);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}