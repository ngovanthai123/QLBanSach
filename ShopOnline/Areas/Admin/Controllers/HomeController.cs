using ShopOnline.Libs;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

		// GET: Home/Login
		public ActionResult Login()
		{
			ModelState.AddModelError("LoginError", "");
			return View();
		}

		// POST: Home/Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(NhanVienLogin nhanVien)
		{
			if (ModelState.IsValid)
			{
				string matKhauMaHoa = SHA1.ComputeHash(nhanVien.MatKhau);
				var taiKhoan = db.NhanVien.Where(r => r.TenDangNhap == nhanVien.TenDangNhap && r.MatKhau == matKhauMaHoa).SingleOrDefault();

				if (taiKhoan == null)
				{
					ModelState.AddModelError("LoginError", "Tên đăng nhập hoặc mật khẩu không chính xác!");
					return View(nhanVien);
				}
				else
				{
					// Đăng ký SESSION
					Session["MaNhanVien"] = taiKhoan.ID;
					Session["HoTenNhanVien"] = taiKhoan.HoVaTen;
					Session["TenDangNhap"] = taiKhoan.TenDangNhap;
					Session["Quyen"] = taiKhoan.Quyen;

					// Quay về trang chủ
					return RedirectToAction("Index", "Home");
				}
			}

			return View(nhanVien);
		}
		public ActionResult ChangePassword()
		{
			ModelState.AddModelError("ChangePassword", "");
			return View();
		}

		// POST: Home/Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ChangePassword(NhanVienChangePassword nhanvien)
		{

			if (ModelState.IsValid)
			{

				string matKhauCu = SHA1.ComputeHash(nhanvien.MatKhauCu);
				string matKhauMoi = SHA1.ComputeHash(nhanvien.MatKhauMoi);
				string xacnhanmatkhau = SHA1.ComputeHash(nhanvien.XacNhanMatKhau);
				string tendangnhap = Session["TenDangNhap"].ToString();
				var taiKhoan = db.NhanVien.Where(r => r.TenDangNhap == tendangnhap && r.MatKhau == matKhauCu).SingleOrDefault();

				if (taiKhoan == null)
				{
					ModelState.AddModelError("ChangePassword", "Tên đăng nhập hoặc mật khẩu không chính xác!");
					return View(nhanvien);
				}
				else
				{
					if (matKhauMoi == xacnhanmatkhau)
					{
						taiKhoan.MatKhau = matKhauMoi;
						taiKhoan.XacNhanMatKhau = matKhauMoi;
						db.Entry(taiKhoan).State = EntityState.Modified;
						db.SaveChanges();
						ModelState.AddModelError("ChangePasswordSucess", "Đổi mật khẩu thành công");
						return View(nhanvien);
					}
				}
			}
			return View(nhanvien);
		}
		[HttpPost]
		public ActionResult Search(FormCollection collection)
		{
			// contains chứa
			string text_search = collection["TuKhoa"].ToString();

			var baikiem_search = db.Sach.Where(r => r.SoLuong > 1 && (r.TenSach.Contains(text_search))).ToList();
			return View(baikiem_search);
		}

		public ActionResult Logout()
		{
			// Xóa SESSION
			Session.RemoveAll();

			// Quay về trang chủ
			return RedirectToAction("Index", "Home");
		}
	}
}