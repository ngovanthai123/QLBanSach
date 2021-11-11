using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class SanPhamTrongGio
    {
        public Sach sAch { get; set; }
        public int soLuongTrongGio { get; set; }
    }

    public class DonHangCuaToi
    {
        public int ID { get; set; }
        public string TenSach { get; set; }
        public string HinhAnhBia { get; set; }
        public HttpPostedFileBase DuLieuHinhAnhBia { get; set; }
        public Nullable<int> DonGia { get; set; }
        public Nullable<short> SoLuong { get; set; }
        public Nullable<System.DateTime> NgayDatHang { get; set; }
        public Nullable<short> TinhTrang { get; set; }

    }
}