using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ShopOnline.Libs;
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

        // GET: THuKHo/Create
        public ActionResult Create()
        {
            return View();
        }
        // GET: THụKHo/Logout
        public ActionResult Logout()
        {
            // Xóa SESSION
            Session.RemoveAll();

            // Quay về trang chủ
            return RedirectToAction("Index", "Home");
        }
        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HoVaTen,DienThoai,DiaChi,Email,TenDangNhap,MatKhau,XacNhanMatKhau")] ThuKho thuKho)
        {
            if (ModelState.IsValid)
            {
                thuKho.MatKhau = SHA1.ComputeHash(thuKho.MatKhau);
                thuKho.XacNhanMatKhau = SHA1.ComputeHash(thuKho.XacNhanMatKhau);
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
        public ActionResult Edit([Bind(Include = "ID,HoVaTen,DienThoai,DiaChi,Email,TenDangNhap,MatKhau,XacNhanMatKhau")] ThuKhoEdit thuKho)
        {
            if (ModelState.IsValid)
            {
                ThuKho n = db.ThuKho.Find(thuKho.ID);

                // Giữ nguyên mật khẩu cũ
                if (thuKho.MatKhau == null)
                {
                    n.ID = thuKho.ID;
                    n.HoVaTen = thuKho.HoVaTen;
                    n.DienThoai = thuKho.DienThoai;
                    n.DiaChi = thuKho.DiaChi;
                    n.Email = thuKho.Email;
                    n.TenDangNhap = thuKho.TenDangNhap;
                    n.XacNhanMatKhau = n.MatKhau;
                }
                else // Cập nhật mật khẩu mới
                {
                    n.ID = thuKho.ID;
                    n.HoVaTen = thuKho.HoVaTen;
                    n.DienThoai = thuKho.DienThoai;
                    n.DiaChi = thuKho.DiaChi;
                    n.Email = thuKho.Email;
                    n.TenDangNhap = thuKho.TenDangNhap;
                    n.MatKhau = SHA1.ComputeHash(thuKho.MatKhau);
                    n.XacNhanMatKhau = SHA1.ComputeHash(thuKho.XacNhanMatKhau);
                }
                db.Entry(n).State = EntityState.Modified;
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
            DatHang datHang = db.DatHang.Find(id);
            if (datHang != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                NhanVien nhanVien = db.NhanVien.Find(id);
                db.NhanVien.Remove(nhanVien);
                db.SaveChanges();
            }
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
