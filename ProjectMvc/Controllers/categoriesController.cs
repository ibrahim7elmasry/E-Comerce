using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectMvc.Models;
using PagedList;
using PagedList.Mvc;
using ProjectMvc.ViewMdel;
using System.IO;

namespace ProjectMvc.Controllers
{
    public class categoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: categories
        public ActionResult Index(int? page)
        {

            IList<category> CategoryList = db.Category.ToList();

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View("Index", CategoryList.ToPagedList<category>(pageNumber, pageSize));
        }

        // GET: categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CategoryId,CategoryName")] category category)
        //{
        //    category FindCategory = db.Category.FirstOrDefault(cat => cat.CategoryName == category.CategoryName);
        //    if (FindCategory != null)
        //    {
        //        ModelState.AddModelError(" ", "this category is already Exist ");
                
        //    }
        //    else { 

        //            if (ModelState.IsValid)
        //            {
        //                      db.Category.Add(category);
        //                    db.SaveChanges();
        //                    return RedirectToAction("Index");
        //            }
        //    }
        //    return View(category);


        //}

        [HttpPost]
        public ActionResult Add(CategoryViewModel cateView)
        {
            string fileName = Path.GetFileNameWithoutExtension(cateView.imageFile.FileName);
            string Extention = Path.GetExtension(cateView.imageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + Extention;
            cateView.CategoryImage = "/Content/images/category/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Content/images/category/"), fileName);
            cateView.imageFile.SaveAs(fileName);

            category newCategory = new category
            {

                CategoryName = cateView.CategoryName,
                CategoryImage = cateView.CategoryImage
                
            };

            db.Category.Add(newCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(CategoryViewModel cateView)
        {
            string fileName = Path.GetFileNameWithoutExtension(cateView.imageFile.FileName);
            string Extention = Path.GetExtension(cateView.imageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + Extention;
            cateView.CategoryImage = "/Content/images/category/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Content/images/category/"), fileName);
            cateView.imageFile.SaveAs(fileName);

            category UpdatedCategory = db.Category.FirstOrDefault(s => s.CategoryId == cateView.CategoryId);
            UpdatedCategory.CategoryName = cateView.CategoryName;
            UpdatedCategory.CategoryImage = cateView.CategoryImage;
            
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName")] category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            category category = db.Category.Find(id);
            db.Category.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
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
