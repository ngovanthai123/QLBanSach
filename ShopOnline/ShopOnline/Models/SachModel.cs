using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class SachModel
    {
        [Display(Name = "Mã sách")]
        public int ID { get; set; }

        [Display(Name = "Nhà xuất bản")]
        [Required(ErrorMessage = "Chưa chọn nhà xuất bản!")]
        public Nullable<int> NhaXuatBan_ID { get; set; }

        [Display(Name = "Loại sách")]
        [Required(ErrorMessage = "Chưa chọn loại sách!")]
        public Nullable<int> LoaiSach_ID { get; set; }

        [Display(Name = "Tên sách")]
        [Required(ErrorMessage = "Tên sách không được bỏ trống!")]
        public string TenSach { get; set; }

        [Display(Name = "Đơn giá")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Đơn giá bản không được bỏ trống!")]
        public Nullable<int> DonGia { get; set; }

        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Số lượng không được bỏ trống!")]
        public Nullable<int> SoLuong { get; set; }

        [Display(Name = "Hình ảnh bìa")]
        public string HinhAnhBia { get; set; }

        [Display(Name = "Hình ảnh mẫu")]
        public HttpPostedFileBase DuLieuHinhAnhBia { get; set; }

        [Display(Name = "Mô tả sách")]
        [DataType(DataType.MultilineText)]
        public string MoTa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatHang_ChiTiet> DatHang_ChiTiet { get; set; }
        public virtual LoaiSach LoaiSach { get; set; }
        public virtual NhaXuatBan NhaXuatBan { get; set; }
    }
}