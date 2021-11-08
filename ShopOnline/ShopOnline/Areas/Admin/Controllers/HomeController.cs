using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private ShopOnlineEntities db = new ShopOnlineEntities();
        public ActionResult Index()
        {
            var t = db.Sach.Where(r => r.SoLuong > 0);
            return View(t);
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