using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Models;

namespace ShopOnline.Areas.AdminTK.Controllers
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

		// GET: Sach/Create
		public ActionResult Create()
		{
			ViewBag.LoaiSach_ID = new SelectList(db.LoaiSach, "ID", "TenLoai");
			ViewBag.NhaXuatBan_ID = new SelectList(db.NhaXuatBan, "ID", "TenNhaXuatBan");
			ModelState.AddModelError("UploadError", "");
			return View();
		}

		// POST: Sach/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "ID,NhaXuatBan_ID,LoaiSach_ID,TenSach,DonGia,SoLuong,DuLieuHinhAnhBia,MoTa")] Sach sach)
		{
			ViewBag.LoaiSach_ID = new SelectList(db.LoaiSach, "ID", "TenLoai", sach.LoaiSach_ID);
			ViewBag.NhaXuatBan_ID = new SelectList(db.NhaXuatBan, "ID", "TenNhaXuatBan", sach.NhaXuatBan_ID);

			if (ModelState.IsValid)
			{
				// Upload
				if (sach.DuLieuHinhAnhBia != null)
				{
					string folder = "Storage/";
					string fileExtension = Path.GetExtension(sach.DuLieuHinhAnhBia.FileName).ToLower();

					// Kiểm tra kiểu
					var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
					if (!fileTypeSupported.Contains(fileExtension))
					{
						ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin JPG, PNG, GIF!");
						return View(sach);
					}
					else if (sach.DuLieuHinhAnhBia.ContentLength > 2 * 1024 * 1024)
					{
						ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin từ 2MB trở xuống!");
						return View(sach);
					}
					else
					{
						string fileName = Guid.NewGuid().ToString() + fileExtension;
						string filePath = Path.Combine(Server.MapPath("~/" + folder), fileName);
						sach.DuLieuHinhAnhBia.SaveAs(filePath);

						// Cập nhật đường dẫn vào CSDL
						sach.HinhAnhBia = folder + fileName;

						db.Sach.Add(sach);
						db.SaveChanges();
						return RedirectToAction("Index");
					}
				}
				else
				{
					ModelState.AddModelError("UploadError", "Hình ảnh bìa không được bỏ trống!");
					return View(sach);
				}
			}

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
			ModelState.AddModelError("UploadError", "");
			return View(sach);
		}

		// POST: Sach/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ID,HinhAnhBia,NhaXuatBan_ID,LoaiSach_ID,TenSach,DonGia,SoLuong,DuLieuHinhAnhBia,MoTa")] Sach sach)
		{
			ViewBag.LoaiSach_ID = new SelectList(db.LoaiSach, "ID", "TenLoai", sach.LoaiSach_ID);
			ViewBag.NhaXuatBan_ID = new SelectList(db.NhaXuatBan, "ID", "TenNhaXuatBan", sach.NhaXuatBan_ID);

			if (ModelState.IsValid)
			{
				// Upload ảnh mới
				if (sach.DuLieuHinhAnhBia != null)
				{
					// Xóa ảnh cũ
					string oldFilePath = Server.MapPath("~/" + sach.HinhAnhBia);
					if (System.IO.File.Exists(oldFilePath)) System.IO.File.Delete(oldFilePath);

					string folder = "Storage/";
					string fileExtension = Path.GetExtension(sach.DuLieuHinhAnhBia.FileName).ToLower();

					// Kiểm tra kiểu
					var fileTypeSupported = new[] { ".jpg", ".jpeg", ".png", ".gif" };
					if (!fileTypeSupported.Contains(fileExtension))
					{
						ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin JPG, PNG, GIF!");
						return View(sach);
					}
					else if (sach.DuLieuHinhAnhBia.ContentLength > 2 * 1024 * 1024)
					{
						ModelState.AddModelError("UploadError", "Chỉ cho phép tập tin từ 2MB trở xuống!");
						return View(sach);
					}
					else
					{
						string fileName = Guid.NewGuid().ToString() + fileExtension;
						string filePath = Path.Combine(Server.MapPath("~/" + folder), fileName);
						sach.DuLieuHinhAnhBia.SaveAs(filePath);

						// Cập nhật đường dẫn vào CSDL
						sach.HinhAnhBia = folder + fileName;

						db.Entry(sach).State = EntityState.Modified;
						db.SaveChanges();
						return RedirectToAction("Index");
					}
				}
				else // Giữ nguyên ảnh cũ
				{
					db.Entry(sach).State = EntityState.Modified;
					db.SaveChanges();
					return RedirectToAction("Index");
				}
			}

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
