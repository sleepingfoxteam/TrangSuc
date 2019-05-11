using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrangSucSolution.Models
{
    public class GioHangItem
    {
        private TrangSuc item;
        private int soluong;

        public GioHangItem(TrangSuc item, int soluong)
        {
            this.Item = item;
            this.Soluong = soluong;
        }

        public int Soluong { get => soluong; set => soluong = value; }
        public TrangSuc Item { get => item; set => item = value; }
    }
}