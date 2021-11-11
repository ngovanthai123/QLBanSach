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

		public ActionResult ChiTiet(int masp)
		{
			var ChiTiet = (from sp in db.Sach
						   where (sp.ID == masp)

						   select new SachModel()
						   {
							   ID = masp,
							   TenSach = sp.TenSach,
							   HinhAnhBia = sp.HinhAnhBia,
							   MoTa = sp.MoTa,
							   NhaXuatBan = sp.NhaXuatBan,



						   }).ToList();

			return View(ChiTiet);
		}

		public ActionResult MuaNhieuNhat()
		{
			var ShopPhone = db.DatHang_ChiTiet.Where(r => r.SoLuong > 0).OrderByDescending(r => r.SoLuong).ToList();
			var MuaNhieuNhat = (from sp in db.Sach
								join chitiet in db.DatHang_ChiTiet on sp.ID equals chitiet.Sach_ID
								where (chitiet.SoLuong > 0)
								select new SachModel()
								{
									TenSach = sp.TenSach,
									HinhAnhBia = sp.HinhAnhBia,
									DonGia = sp.DonGia,
									ID = sp.ID,
									SoLuong = chitiet.SoLuong
								}).OrderByDescending(chitiet => chitiet.SoLuong).ToList();

			return View(MuaNhieuNhat);
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
		public ActionResult ChangePassword([Bind(Include = "MatKhau,MatKhauMoi,XacNhanMatKhauMoi")] ChangePassword ChangePassword)
		{
			if (ModelState.IsValid)
			{
				int makh = Convert.ToInt32(Session["MaKhachHang"]);
				KhachHang khachHang = db.KhachHang.Find(makh);
				if (khachHang == null)
				{
					return HttpNotFound();
				}
				ChangePassword.MatKhauCu = SHA1.ComputeHash(ChangePassword.MatKhauCu);
				if (khachHang.MatKhau == ChangePassword.MatKhauCu)
				{
					ChangePassword.MatKhauMoi = SHA1.ComputeHash(ChangePassword.MatKhauMoi);
					ChangePassword.XacNhanMatKhau = ChangePassword.MatKhauMoi;

					khachHang.MatKhau = ChangePassword.MatKhauMoi;
					khachHang.XacNhanMatKhau = ChangePassword.MatKhauMoi;

					db.Entry(khachHang).State = EntityState.Modified;
					db.SaveChanges();
					return RedirectToAction("Logout", "Home");
				}
				else
				{
					ViewBag.error = "Mật khẩu cũ không đúng !!!";
					return View();
				}


			}
			return View(ChangePassword);
		}


		public ActionResult KhachHangSignUp()
		{
			ModelState.AddModelError("SignUpError", "");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult KhachHangSignUp(KhachHang kh)
		{
			if (ModelState.IsValid)
			{
				var check = db.KhachHang.FirstOrDefault(r => r.TenDangNhap == kh.TenDangNhap);
				if (check == null)
				{
					kh.MatKhau = SHA1.ComputeHash(kh.MatKhau);
					kh.XacNhanMatKhau = SHA1.ComputeHash(kh.XacNhanMatKhau);
					db.Configuration.ValidateOnSaveEnabled = false;
					db.KhachHang.Add(kh);
					db.SaveChanges();
					return RedirectToAction("Login");
				}
				else
				{
					ViewBag.error = "Tên đăng nhập đã tồn tại !!!";
					return View();
				}
			}
			return View();
		}


		public ActionResult ThanhToan()
		{
			if (Session["MaKhachHang"] == null)
			{
				return RedirectToAction("GioHang", "Home");
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
			/*int makh = Convert.ToInt32(Session["MaKhachHang"].ToString());
			var dh = db.DatHang_ChiTiet.Where(r => r.DatHang.KhachHang_ID == makh && r.DatHang.TinhTrang != 3).ToList();
			return View(dh);*/

			int makh = Convert.ToInt32(Session["MaKhachHang"]);
			var DonHangCuaToi = (from sp in db.Sach
								 join chitiet in db.DatHang_ChiTiet on sp.ID equals chitiet.Sach_ID
								 join dhang in db.DatHang on chitiet.DatHang_ID equals dhang.ID
								 join kh in db.KhachHang on dhang.KhachHang_ID equals kh.ID
								 where (kh.ID == makh)

								 select new DonHangCuaToi()
								 {
									 TenSach = sp.TenSach,
									 HinhAnhBia = sp.HinhAnhBia,
									 DonGia = chitiet.DonGia,
									 ID = kh.ID,
									 SoLuong = chitiet.SoLuong,
									 NgayDatHang = dhang.NgayDatHang

								 }).OrderByDescending(dhang => dhang.NgayDatHang).ToList();

			return View(DonHangCuaToi);
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