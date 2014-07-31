using Gameblog.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Gameblog.Controllers
{
    public class HomeController : Controller
    {

        private MydbEntities db = new MydbEntities();
       
        public ActionResult Index()
        {

            ViewData["categories"] = db.Categories;
            ViewData["photos"] = db.Photos; 
            ViewData["Reviews"] = db.Reviews.Include(r => r.Photos);
            return View();
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