using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gameblog.Models;
using System.Web.Helpers;

namespace Gameblog.Controllers
{
    public class PhotosController : Controller
    {
        private MydbEntities db = new MydbEntities();

        // GET: Photos
        public ActionResult Index()
        {
            var photos = db.Photos.Include(p => p.Review);
            return View(photos.ToList());
        }

        // GET: Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: Photos/Create
        public ActionResult Create()
        {
            ViewBag.Review_id = new SelectList(db.Reviews, "id", "name", "Review_id");
            return View();
        }

        // POST: Photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,Review_id,imageName")] Photo photo, HttpPostedFileBase file)
        {

            //Initialize our image file 


            WebImage image = null;

            var newFileName = "";

            var imagePath = "";

            var imageThumbPath = "";

            image = WebImage.GetImageFromRequest();

            if (image != null)
            {
                //get Path add image 

                newFileName = Guid.NewGuid().ToString() + "_" +

                Path.GetFileName(image.FileName);

                imagePath = @"images\" + newFileName;

                image.Save(@"~\" + imagePath);

                imageThumbPath = @"images\thumbs\" + newFileName;

                image.Resize(width: 60, height: 60, preserveAspectRatio: true,

                preventEnlarge: true);

                image.Save(@"~\" + imageThumbPath);

            }

            //save model object in sql Db

            if (ModelState.IsValid)
            {
                //create timestamp for photo 
                DateTime now = new DateTime();
                now = DateTime.Now;
                photo.timeStamp = now;
                //add fileName
                photo.imageName = newFileName;
                //save our object with timestamp
                db.Photos.Add(photo);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.Review_id = new SelectList(db.Reviews, "id", "name", photo.Review_id);
            return View(photo);
        }

        // GET: Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Review_id = new SelectList(db.Reviews, "id", "name", photo.Review_id);
            return View(photo);
        }

        // POST: Photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,Review_id,imageName,timeStamp")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Review_id = new SelectList(db.Reviews, "id", "name", photo.Review_id);
            return View(photo);
        }

        // GET: Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
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
