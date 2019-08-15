using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectMvc.Models;

namespace ProjectMvc.Controllers
{
    public class showproductitemController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        // GET: showproductitem

        public ActionResult Index( int id)
        {
            Product txt = context.Product.FirstOrDefault(id1 => id1.ProductId == id);
            List<string> imgs = new List<string>();
            List<ImagesProduct> imgcollection = context.ImagesProduct.ToList();
            foreach(var item in imgcollection)
            {
                imgs.Add(item.ImagesUrl);
               
            }
            ViewBag.listimg = imgs;
            return View(txt);
        }
    }
}