using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectMvc.Models;
using ProjectMvc.viewModel;

namespace ProjectMvc.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext context = new ApplicationDbContext();


        //--------------------------------first IBRAHIM CODE--------------------------------------------
        public ActionResult addheart(int d1)
        {
            if (Session["heart"] == null)
            {
                List<item> cart = new List<item>();
                //Product prd = db.Product.Find(id);
                Product prd = context.Product.FirstOrDefault(p => p.ProductId == d1);

                cart.Add(new item { ProductItem = prd, Quantity = 7 });

                Session["heart"] = cart;

            }

            else
            {
                List<item> cart = (List<item>)Session["heart"];
                int index = isExit(d1);

                if (index != -1)
                {
                    cart[index].Quantity++;
                }

                else
                {
                    cart.Add(new item { ProductItem = context.Product.Find(d1), Quantity = 7 });
                }

                Session["heart"] = cart;
            }


            return View("_heart");

        }

        public ActionResult AddToCart(int id)
        {
            int counter = 0;
            if (Session["cart"] == null)
            {
                List<item> cart = new List<item>();
                Product prd = context.Product.FirstOrDefault(p => p.ProductId == id);
                foreach (item it1 in (List<item>)Session["cart"])
                {
                    if (it1.ProductItem.ProductId == prd.ProductId)
                    {
                        it1.ProductItem.Quantity++;
                    }
                }
                cart.Add(new item { ProductItem = prd, Quantity = 7 });

                Session["cart"] = cart;

            }

            else
            {
                List<item> cart = (List<item>)Session["cart"];
                int index = isExit(id);

                if (index != -1)
                {
                    counter++;
                    cart[index].Quantity++;
                }

                else
                {
                    cart.Add(new item { ProductItem = context.Product.Find(id), Quantity = 7 });
                }

                Session["cart"] = cart;

            }


            return View("AddToCart");

        }

        //----------------------------if exists------------------------------
        private int isExit(int prdId)
        {
            List<item> cart = (List<item>)Session["cart"];

            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductItem.ProductId.Equals(prdId))
                {
                    return i;
                }

            }
            return -1;

        }
        //----------------------------------------------------------------------

        public ActionResult RemoveCart(int prdId)
        {
            List<item> cart = (List<item>)Session["cart"];

            int index = isExit(prdId);

            cart.RemoveAt(index);
            Session["cart"] = cart;

            return RedirectToAction("AddToCart");

        }



        public ActionResult ViewCart()
        {
            return View(Session["cart"]);
        }

        public ActionResult Checkout()
        {

            return View("Checkout", Session["cart"]);
        }





        //------------------------------End IBRAHIM CODE-------------------------------------------------

        //Get product of one category
        [HttpGet]
        public ActionResult GetProducts(int CatId)
        {
            List<ProductWithImag> MyProductWithImages = new List<ProductWithImag>();
            ProductWithImag ProductWithImagObejct;
            category slectedCategory = context.Category.FirstOrDefault(cat => cat.CategoryId == CatId);
            List<Product> ProductList = slectedCategory.ProductLis.ToList();

            foreach (var item in ProductList)
            {

                ProductWithImagObejct = new ProductWithImag();
                ProductWithImagObejct.ProductId = item.ProductId;
                ProductWithImagObejct.productName = item.productName;
                ProductWithImagObejct.Price = item.Price;
                ProductWithImagObejct.Description = item.Description;
                ProductWithImagObejct.Quantity = item.Quantity;
                ProductWithImagObejct.ProductDetails = item.ProductDetails;
                if (item.IamgesList.Count != 0)
                {
                    ProductWithImagObejct.ImagesUrl = item.IamgesList.ElementAt(0).ImagesUrl;
                }
                else
                {
                    ProductWithImagObejct.ImagesUrl = @"\Content\images\PoductImages\Dummy.jpg";
                }

                MyProductWithImages.Add(ProductWithImagObejct);

            }
            return PartialView("_PartialProductsBlock", MyProductWithImages);
        }

        public ActionResult Index()
        {
            List<ProductWithImag> MyProductWithImages = new List<ProductWithImag>();
            ProductWithImag ProductWithImagObejct;
            //List<string> Images = new List<string>();

            //Get list of all category
            List<category> CategoryList = context.Category.ToList();
            //Get first Category
            category FirstCategory = context.Category.FirstOrDefault();
            List<Product> data = FirstCategory.ProductLis.ToList();

            //Get First Slider
            SliderImage FirstSlider = context.SliderImage.FirstOrDefault();
            //List<Product> data = context.Product.ToList();
            
            //fill List of ProductWithImages 
            foreach (var item in data)
            {
               
                ProductWithImagObejct = new ProductWithImag();
                ProductWithImagObejct.ProductId = item.ProductId;
                ProductWithImagObejct.productName = item.productName;
                ProductWithImagObejct.Price = item.Price;
                ProductWithImagObejct.Description = item.Description;
                ProductWithImagObejct.Quantity = item.Quantity;
                ProductWithImagObejct.ProductDetails = item.ProductDetails;
                if (item.IamgesList.Count != 0)
                {
                    ProductWithImagObejct.ImagesUrl = item.IamgesList.ElementAt(0).ImagesUrl;
                }
                else
                {
                    ProductWithImagObejct.ImagesUrl = @"\Content\images\PoductImages\Dummy.jpg";
                }

                MyProductWithImages.Add(ProductWithImagObejct);
            
            }

            CatProductWithImageSliderViewModel ViewObj = new CatProductWithImageSliderViewModel();

            ViewObj.ListOfCategory = CategoryList;
            ViewObj.ProductWithImagList = MyProductWithImages;
            ViewObj.SliderImageObg = FirstSlider;

            //ViewData["firstIamge"] = Images;//list of URL of ALl Image of product
            //  return View(MyProductWithImages);
            return View(ViewObj);
            



        }

        //---------------------------Get Products of category-------------------------------

            [HttpGet]
        public ActionResult GetproductsOfOneCategory(int id)
        {
          List<Product>ProductList =  context.Product.Where(P => P.CategoryObj.CategoryId == id).ToList();
            List<ProductWithImag> MyProductWithImages = new List<ProductWithImag>();
            ProductWithImag ProductWithImagObejct;
            foreach (var item in ProductList)
            {

                ProductWithImagObejct = new ProductWithImag();
                ProductWithImagObejct.ProductId = item.ProductId;
                ProductWithImagObejct.productName = item.productName;
                ProductWithImagObejct.Price = item.Price;
                ProductWithImagObejct.Description = item.Description;
                ProductWithImagObejct.Quantity = item.Quantity;
                ProductWithImagObejct.ProductDetails = item.ProductDetails;
                if (item.IamgesList.Count != 0)
                {
                    ProductWithImagObejct.ImagesUrl = item.IamgesList.ElementAt(0).ImagesUrl;
                }
                else
                {
                    ProductWithImagObejct.ImagesUrl = @"\Content\images\PoductImages\Dummy.jpg";
                }

                MyProductWithImages.Add(ProductWithImagObejct);

            }
            
            return View("GetproductsOfOneCategory", MyProductWithImages);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}