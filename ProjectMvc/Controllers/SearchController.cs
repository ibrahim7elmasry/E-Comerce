using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectMvc.Models;

namespace ProjectMvc.Controllers
{
    public class SearchController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Search
        public ActionResult SearchResult(string selection, string searchTxtBox)
        {
            try
            {
                int selectedCat = int.Parse(selection);

                //if a category is selected and no search text entered
                if (searchTxtBox.Length == 0 && selectedCat != -1)
                {
                    List<Product> PList = db.Product.Where(p => p.CategoryObj.CategoryId == selectedCat + 1).ToList();

                    return View(PList);
                }

                //if all categories is selected and no search text is entered
                else if (selectedCat == -1 && searchTxtBox.Length == 0)
                {
                    return RedirectToAction("Index", "Home");
                }

                //if all categories is selected and a search text is entered ==> search in all products 
                else if (selectedCat == -1 && searchTxtBox.Length != 0)
                {
                    List<Product> PList = db.Product.Where(p => p.productName.Contains(searchTxtBox)).ToList();

                    return View(PList);
                }

                //if a category is selected and a search text is entered
                else if (selectedCat > -1 && searchTxtBox.Length != 0)
                {
                    List<Product> PList =
                        db.Product.Where(p => p.CategoryObj.CategoryId == selectedCat + 1
                            && p.productName.Contains(searchTxtBox)).ToList();

                    return View(PList);
                }

                //if none of the above conditions returns true
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }


        }
    }
}