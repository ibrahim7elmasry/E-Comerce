using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectMvc.viewModel;
using ProjectMvc.Models;

namespace ProjectMvc.Controllers
{
    public class listitemproductController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        // GET: listitemproduct
        public ActionResult Index()
        {
            List<ProductWithImag> MyProductWithImages = new List<ProductWithImag>();
            ProductWithImag ProductWithImagObejct;
            List<string> Images = new List<string>();
            List<Product> data = context.Product.ToList();
            foreach (var item in data)
            {

                ProductWithImagObejct = new ProductWithImag();
                ProductWithImagObejct.ProductId = item.ProductId;
                ProductWithImagObejct.productName = item.productName;
                ProductWithImagObejct.Price = item.Price;
                ProductWithImagObejct.Description = item.Description;
                ProductWithImagObejct.Quantity = item.Quantity;
                ProductWithImagObejct.ProductDetails = item.ProductDetails;
                ProductWithImagObejct.ImagesUrl = item.IamgesList.ElementAt(0).ImagesUrl;

                MyProductWithImages.Add(ProductWithImagObejct);
            }

            return View(MyProductWithImages);
        }
    }
}