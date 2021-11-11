using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Models;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class ThuKhoController : Controller
    {
        private ShopOnlineEntities db = new ShopOnlineEntities();

        // GET: ThuKho
        public ActionResult Index()
        {
            return View(db.ThuKho.ToList());
        }

        // GET: ThuKho/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuKho thuKho = db.ThuKho.Find(id);
            if (thuKho == null)
            {
                return HttpNotFound();
            }
            return View(thuKho);
        }

        // GET: ThuKho/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ThuKho/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HoVaTen,DienThoai,DiaChi,Email,TenDangNhap,MatKhau")] ThuKho thuKho)
        {
            if (ModelState.IsValid)
            {
                db.ThuKho.Add(thuKho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thuKho);
        }

        // GET: ThuKho/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuKho thuKho = db.ThuKho.Find(id);
            if (thuKho == null)
            {
                return HttpNotFound();
            }
            return View(thuKho);
        }

        // POST: ThuKho/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HoVaTen,DienThoai,DiaChi,Email,TenDangNhap,MatKhau")] ThuKho thuKho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thuKho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thuKho);
        }

        // GET: ThuKho/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuKho thuKho = db.ThuKho.Find(id);
            if (thuKho == null)
            {
                return HttpNotFound();
            }
            return View(thuKho);
        }

        // POST: ThuKho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThuKho thuKho = db.ThuKho.Find(id);
            db.ThuKho.Remove(thuKho);
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
