using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectMvc.Models;
using System.IO;
using ProjectMvc.ViewMdel;

namespace ProjectMvc.Controllers
{
    public class SliderImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SliderImages
        public ActionResult Index()
        {
            return View(db.SliderImage.ToList());
        }

        // GET: SliderImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderImage sliderImage = db.SliderImage.Find(id);
            if (sliderImage == null)
            {
                return HttpNotFound();
            }
            return View(sliderImage);
        }

        // GET: SliderImages/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: SliderImages/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "sliderId,sliderTile,sliderimage")] SliderImage sliderImage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.SliderImage.Add(sliderImage);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(sliderImage);
        //}

        [HttpPost]
        public ActionResult Add(ImageSliderViewModel sliderImageView)
        {
            string fileName = Path.GetFileNameWithoutExtension(sliderImageView.imageFile.FileName);
            string Extention = Path.GetExtension(sliderImageView.imageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff")+Extention;
            sliderImageView.sliderimage = "/Content/images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
            sliderImageView.imageFile.SaveAs(fileName);

            SliderImage newSliderImage = new SliderImage
            {
                sliderTile = sliderImageView.sliderTile,
                sliderimage = sliderImageView.sliderimage
            };
            
            db.SliderImage.Add(newSliderImage);
            db.SaveChanges();
                return RedirectToAction("Index");
        }

        public ActionResult Update(ImageSliderViewModel sliderImageView)
        {
            string fileName = Path.GetFileNameWithoutExtension(sliderImageView.imageFile.FileName);
            string Extention = Path.GetExtension(sliderImageView.imageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + Extention;
            sliderImageView.sliderimage = "/Content/images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
            sliderImageView.imageFile.SaveAs(fileName);

            SliderImage UpdatedSliderImage = db.SliderImage.FirstOrDefault(s => s.sliderId == sliderImageView.sliderId);
            UpdatedSliderImage.sliderTile = sliderImageView.sliderTile;
            UpdatedSliderImage.sliderimage = sliderImageView.sliderimage;

            db.SaveChanges();
            return RedirectToAction("Index");

        }
        // GET: SliderImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderImage sliderImage = db.SliderImage.Find(id);
            if (sliderImage == null)
            {
                return HttpNotFound();
            }
            return View(sliderImage);
        }

        // POST: SliderImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sliderId,sliderTile,sliderimage")] SliderImage sliderImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sliderImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sliderImage);
        }

        // GET: SliderImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderImage sliderImage = db.SliderImage.Find(id);
            if (sliderImage == null)
            {
                return HttpNotFound();
            }
            return View(sliderImage);
        }

        // POST: SliderImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SliderImage sliderImage = db.SliderImage.Find(id);
            db.SliderImage.Remove(sliderImage);
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
