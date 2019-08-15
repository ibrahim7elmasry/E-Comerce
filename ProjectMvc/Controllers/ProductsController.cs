using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectMvc.Models;
using ProjectMvc.ViewMdel;
using System.IO;
using ProjectMvc.viewModel;

namespace ProjectMvc.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult UpdateFiles(UpdateProductImage up, int ProductId)
        {

            ImagesProduct ImageProductObject;
            //Ensure model state is valid  
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in up.FileModelObj.files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Content/images/PoductImages/") + InputFileName);
                        string filePath = "/Content/images/PoductImages/" + InputFileName;
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = up.FileModelObj.files.Count().ToString() + " files uploaded successfully.";

                        ImageProductObject = new ImagesProduct
                        {
                            ImagesUrl = filePath,

                        };
                        //  db.ImagesProduct.Add(ImageProductObject);
                        // db.SaveChanges();

                        Product myProduct = db.Product.FirstOrDefault(p => p.ProductId == ProductId);
                        myProduct.IamgesList.Add(ImageProductObject);
                        db.SaveChanges();
                    }




                }

            }
            //return View();
            return RedirectToAction("Index");
        }



        //-----------------------------------------------------------------

        public ActionResult UploadFiles()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase[] files,int ProductId)
        {
           
            ImagesProduct ImageProductObject;
            //Ensure model state is valid  
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Content/images/PoductImages/") + InputFileName);
                        string filePath = "/Content/images/PoductImages/" + InputFileName;
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";

                        ImageProductObject = new ImagesProduct
                        {
                            ImagesUrl = filePath,
                            
                        };
                      //  db.ImagesProduct.Add(ImageProductObject);
                       // db.SaveChanges();

                        Product myProduct = db.Product.FirstOrDefault(p => p.ProductId == ProductId);
                        myProduct.IamgesList.Add(ImageProductObject);
                        db.SaveChanges();
                    }

                   
               
                    
                }

            }
            //return View();
            return RedirectToAction("Index");
        }
        // GET: Products
        public ActionResult Index()
        {
            return View(db.Product.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            List<string> categoryNames = new List<string>();
           List<category>CategoryList = db.Category.ToList();
            foreach (var item in CategoryList)
            {
                categoryNames.Add(item.CategoryName);
            }
            ViewData["categoryNames"] = categoryNames;
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,productName,ProductDetails,CreateDate,ModifedeDate,Description,isFeatured,Quantity,Price")] Product product,string CatName)
        {
            category SelectdCategory = db.Category.FirstOrDefault(c => c.CategoryName == CatName);
            
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
              
                SelectdCategory.ProductLis.Add(product);
                db.SaveChanges();
                // return RedirectToAction("Index");
                ViewData["ProductID"] = product.ProductId;
                return View("UploadFiles");
            }
            

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,productName,ProductDetails,CreateDate,ModifedeDate,Description,isFeatured,Quantity,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<ImagesProduct>ImageProductsList = db.ImagesProduct.Where(I => I.ProductObject.ProductId == id).ToList();
            foreach (var item in ImageProductsList)
            {
                db.ImagesProduct.Remove(item);
                db.SaveChanges();
            }
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ImagesUpdate(int id)
        {
          List<ImagesProduct> images =  db.ImagesProduct.Where(I => I.ProductObject.ProductId == id).ToList();
            UpdateProductImage updProIma = new UpdateProductImage();
            updProIma.ImagesProductList = images;
            ViewData["ProductID"] = id;
            return View(updProIma);
        }
        //dlelte image
        public ActionResult DeleteImage(int id)
        {
            ImagesProduct DletedImage = db.ImagesProduct.FirstOrDefault(Image => Image.ID == id);
           
          
            int productId = DletedImage.ProductObject.ProductId;
           Product myproduct = db.Product.FirstOrDefault(p => p.ProductId == productId);
            db.ImagesProduct.Remove(DletedImage);
            db.SaveChanges();
            return RedirectToAction ("ImagesUpdate",new {id= myproduct.ProductId } );



        }

        public ActionResult Udateimage(int id)
        {
            ViewData["ProductID"] = id;
            return View("UploadFiles");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
