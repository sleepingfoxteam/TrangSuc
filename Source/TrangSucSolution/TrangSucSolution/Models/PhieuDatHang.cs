//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrangSucSolution.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PhieuDatHang
    {
        public string SoPhieu { get; set; }
        public string MaMatHang { get; set; }
        public string TenMatHang { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<int> DonGia { get; set; }
        public Nullable<int> TongGiaTri { get; set; }
        public Nullable<System.DateTime> NgayLap { get; set; }
        public string NguoiLap { get; set; }
    
        public virtual NhanVien NhanVien { get; set; }
        public virtual TrangSuc TrangSuc { get; set; }
    }
}
