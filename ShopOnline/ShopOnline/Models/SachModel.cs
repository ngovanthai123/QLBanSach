using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class SachModel
    {
        public int ID { get; set; }
        public Nullable<int> NhaXuatBan_ID { get; set; }
        public Nullable<int> LoaiSach_ID { get; set; }
        public string TenSach { get; set; }
        public Nullable<int> DonGia { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public string HinhAnhBia { get; set; }
        [Display(Name = "Hình ảnh mẫu")]
        public HttpPostedFileBase DuLieuHinhAnhBia { get; set; }
        public string MoTa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatHang_ChiTiet> DatHang_ChiTiet { get; set; }
        public virtual LoaiSach LoaiSach { get; set; }
        public virtual NhaXuatBan NhaXuatBan { get; set; }
    }
}