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
    public class SachController : Controller
    {
        private ShopOnlineEntities db = new ShopOnlineEntities();

        // GET: Sach
        public ActionResult Index()
        {
            var sach = db.Sach.Include(s => s.LoaiSach).Include(s => s.NhaXuatBan);
            return View(sach.ToList());
        }

        // GET: Sach/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Sach.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // GET: Sach/Create
        public ActionResult Create()
        {
            ViewBag.LoaiSach_ID = new SelectList(db.LoaiSach, "ID", "TenLoai");
            ViewBag.NhaXuatBan_ID = new SelectList(db.NhaXuatBan, "ID", "TenNhaXuatBan");
            return View();
        }

        // POST: Sach/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NhaXuatBan_ID,LoaiSach_ID,TenSach,DonGia,SoLuong,HinhAnhBia,MoTa")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Sach.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoaiSach_ID = new SelectList(db.LoaiSach, "ID", "TenLoai", sach.LoaiSach_ID);
            ViewBag.NhaXuatBan_ID = new SelectList(db.NhaXuatBan, "ID", "TenNhaXuatBan", sach.NhaXuatBan_ID);
            return View(sach);
        }

        // GET: Sach/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Sach.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiSach_ID = new SelectList(db.LoaiSach, "ID", "TenLoai", sach.LoaiSach_ID);
            ViewBag.NhaXuatBan_ID = new SelectList(db.NhaXuatBan, "ID", "TenNhaXuatBan", sach.NhaXuatBan_ID);
            return View(sach);
        }

        // POST: Sach/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NhaXuatBan_ID,LoaiSach_ID,TenSach,DonGia,SoLuong,HinhAnhBia,MoTa")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiSach_ID = new SelectList(db.LoaiSach, "ID", "TenLoai", sach.LoaiSach_ID);
            ViewBag.NhaXuatBan_ID = new SelectList(db.NhaXuatBan, "ID", "TenNhaXuatBan", sach.NhaXuatBan_ID);
            return View(sach);
        }

        // GET: Sach/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Sach.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // POST: Sach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sach sach = db.Sach.Find(id);
            db.Sach.Remove(sach);
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
