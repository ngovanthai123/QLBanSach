using ShopOnline.Libs;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
	public class HomeController : Controller
	{
		private ShopOnlineEntities db = new ShopOnlineEntities();
		public ActionResult Index()
		{
			var t = db.Sach.Where(r => r.SoLuong > 0);
			return View(t);
		}

		public ActionResult ChiTiet(int id)
		{
			var t = db.Sach.Where(r => r.SoLuong > 0 && r.ID == id).SingleOrDefault();
			return View(t);
		}
		public ActionResult Logout()
		{
			Session.Remove("MaKhachHang");
			Session.Remove("HoTenKhachHang");
			Session.Remove("TenDangNhap");
			return RedirectToAction("Login", "Home");
		}

		public ActionResult Login()
		{
			ModelState.AddModelError("LoginError", "");
			return View();
		}



		// POST: Home/Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(KhachHangLogin khachHang)
		{
			if (ModelState.IsValid)
			{
				string matKhauMaHoa = SHA1.ComputeHash(khachHang.MatKhau);
				var taiKhoan = db.KhachHang.Where(r => r.TenDangNhap == khachHang.TenDangNhap && r.MatKhau == matKhauMaHoa).SingleOrDefault();

				if (taiKhoan == null)
				{
					ModelState.AddModelError("LoginError", "Tên đăng nhập hoặc mật khẩu không chính xác!");
					return View(khachHang);
				}
				else
				{
					// Đăng ký SESSION
					Session["MaKhachHang"] = taiKhoan.ID;
					Session["HoTenKhachHang"] = taiKhoan.HoVaTen;
					Session["TenDangNhap"] = taiKhoan.TenDangNhap;
					// Quay về trang chủ
					return RedirectToAction("Index", "Home");
				}
			}

			return View(khachHang);
		}
		public ActionResult ChangePassword()
		{
			ModelState.AddModelError("ChangePassword", "");
			return View();
		}

		// POST: Home/ChangePassword
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ChangePassword(ChangePassword khachHang)
		{

			if (ModelState.IsValid)
			{

				string matKhauCu = SHA1.ComputeHash(khachHang.MatKhauCu);
				string matKhauMoi = SHA1.ComputeHash(khachHang.MatKhauMoi);
				string xacnhanmatkhau = SHA1.ComputeHash(khachHang.XacNhanMatKhau);
				string tendangnhap = Session["TenDangNhap"].ToString();
				var taiKhoan = db.KhachHang.Where(r => r.TenDangNhap == tendangnhap && r.MatKhau == matKhauCu).SingleOrDefault();

				if (taiKhoan == null)
				{
					ModelState.AddModelError("ChangePassword", "Tên đăng nhập hoặc mật khẩu không chính xác!");
					return View(khachHang);
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
						return View(khachHang);
					}
				}
			}
			return View(khachHang);
		}

		public ActionResult KhachHangSignUp()
		{
			ModelState.AddModelError("SignUpError", "");
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult KhachHangSignUp([Bind(Include = "ID,HoVaTen,DienThoai,DiaChi,TenDangNhap,MatKhau,XacNhanMatKhau")] KhachHang khachHang)
		{
			if (ModelState.IsValid)
			{
				khachHang.MatKhau = SHA1.ComputeHash(khachHang.MatKhau);
				khachHang.XacNhanMatKhau = SHA1.ComputeHash(khachHang.XacNhanMatKhau);
				db.KhachHang.Add(khachHang);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(khachHang);
		}





		public ActionResult ThanhToan()
		{
			if (Session["MaKhachHang"] == null)
			{
				return RedirectToAction("Login", "Home");
			}
			else
			{
				return View();
			}
		}

		// POST: Home/ThanhToan
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ThanhToan(DatHang datHang)
		{
			if (ModelState.IsValid)
			{
				// Lưu vào bảng DatHang
				DatHang dh = new DatHang();
				dh.DiaChiGiaoHang = datHang.DiaChiGiaoHang;
				dh.DienThoaiGiaoHang = datHang.DienThoaiGiaoHang;
				dh.NgayDatHang = DateTime.Now;
				dh.KhachHang_ID = Convert.ToInt32(Session["MaKhachHang"]);
				dh.TinhTrang = 0;
				db.DatHang.Add(dh);
				db.SaveChanges();

				// Lưu vào bảng DatHang_ChiTiet
				List<SanPhamTrongGio> cart = (List<SanPhamTrongGio>)Session["cart"];
				foreach (var item in cart)
				{
					DatHang_ChiTiet ct = new DatHang_ChiTiet();

					ct.DatHang_ID = dh.ID;
					ct.Sach_ID = item.sAch.ID;
					ct.SoLuong = Convert.ToInt16(item.soLuongTrongGio);
					ct.DonGia = item.sAch.DonGia;
					db.DatHang_ChiTiet.Add(ct);
					db.SaveChanges();
				}

				// Xóa giỏ hàng
				cart.Clear();

				// Quay về trang chủ
				return RedirectToAction("Index", "Home");
			}

			return View(datHang);
		}

		public ActionResult MyOders()
		{
			int makh = Convert.ToInt32(Session["MaKhachHang"].ToString());
			var dh = db.DatHang_ChiTiet.Where(r => r.DatHang.KhachHang_ID == makh && r.DatHang.TinhTrang != 3).ToList();
			return View(dh);
		}
		[HttpPost]
		public ActionResult Search(FormCollection collection)
		{
			// contains chứa
			string text_search = collection["TuKhoa"].ToString();

			var baikiem_search = db.Sach.Where(r => r.SoLuong > 1 && (r.TenSach.Contains(text_search))).ToList();
			return View(baikiem_search);
		}
	}
}