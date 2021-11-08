﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class DatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DatHang()
        {
            this.DatHang_ChiTiet = new HashSet<DatHang_ChiTiet>();
        }

        [Display(Name = "Mã đặt hàng")]
        public int ID { get; set; }

        [Display(Name = "Nhân viên")]
        [Required(ErrorMessage = "Chưa chọn nhân viên!")]
        public Nullable<int> NhanVien_ID { get; set; }

        [Display(Name = "Khách hàng")]
        [Required(ErrorMessage = "Chưa chọn khách hàng!")]
        public Nullable<int> KhachHang_ID { get; set; }

        [Display(Name = "Điện thoại giao hàng")]
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không đúng định dạng.")]
        [DataType(DataType.PhoneNumber)]
        public string DienThoaiGiaoHang { get; set; }

        [Display(Name = "Địa chỉ giao hàng")]
        [Required(ErrorMessage = "Không được bỏ trống!")]
        public string DiaChiGiaoHang { get; set; }

        [Display(Name = "Ngày đặt hàng")]
        [Required(ErrorMessage = "Ngày đặt hàng được bỏ trống!")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> NgayDatHang { get; set; }

        [Display(Name = "Tình trạng đơn hàng")]
        [Required(ErrorMessage = "Chưa chọn")]
        public Nullable<short> TinhTrang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatHang_ChiTiet> DatHang_ChiTiet { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
