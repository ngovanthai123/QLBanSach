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

    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            this.DatHang = new HashSet<DatHang>();
        }
    
        [Display(Name = "Mã số")]
        public int ID { get; set; }

        [Display(Name ="Họ và tên nhân viên")]
        [Required(ErrorMessage ="Không được bỏ trống!")]
        public string HoVaTen { get; set; }

        [Display(Name ="Số điện thoại")]
        [Required(ErrorMessage ="Không được bỏ trống!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không đúng định dạng.")]
        public string DienThoai { get; set; }

        [Display(Name ="Địa chỉ")]
        [Required(ErrorMessage ="Không được bỏ trống!")]
        public string DiaChi { get; set; }

        [Display(Name ="Tên đăng nhập")]
        [Required(ErrorMessage ="Tên đăng nhập không được để trống!")]
        public string TenDangNhap { get; set; }

        [Display(Name ="Mật khẩu")]
        [Required(ErrorMessage ="Mật khẩu không được để trống!")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Display(Name ="Xác nhận mật khẩu")]
        [Required(ErrorMessage ="không được bỏ trống!")]
        [Compare("MatKhau",ErrorMessage ="Xác nhận mật khẩu không chính xác!")]
        [DataType(DataType.Password)]
        public string XacNhanMatKhau { get; set; }

        [Display(Name ="Quyền hạn")]
        [Required(ErrorMessage ="Chưa chọn quyền hạn")]
        public Nullable<bool> Quyen { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatHang> DatHang { get; set; }
    }
}
