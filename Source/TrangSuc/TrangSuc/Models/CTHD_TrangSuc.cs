//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrangSuc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTHD_TrangSuc
    {
        public string IDHoaDon { get; set; }
        public string IDTrangSuc { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<int> Gia { get; set; }
    
        public virtual HoaDon HoaDon { get; set; }
        public virtual TrangSuc TrangSuc { get; set; }
    }
}
