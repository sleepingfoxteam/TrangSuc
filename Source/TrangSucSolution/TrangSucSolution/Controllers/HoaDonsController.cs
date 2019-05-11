using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TrangSucSolution.Models;

namespace TrangSucSolution.Controllers
{
    public class HoaDonsController : Controller
    {
        private TRANGSUCEntities db = new TRANGSUCEntities();

        static int hdcount = 0;
        public List<SelectListItem> getTinhTrang()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Đã thanh toán", Value = "1" });
            items.Add(new SelectListItem { Text = "Chờ xác nhận", Value = "0" });
            return items;
        }

        // GET: HoaDons
        public ActionResult Index()
        {
            var hoaDons = db.HoaDons.Include(h => h.NhanVien).OrderBy(hd => hd.ID);
            List<SelectListItem> items = getTinhTrang();
            
            ViewBag.TinhTrang = items;
            return View(hoaDons.ToList());
        }

        // GET: HoaDons/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> items = getTinhTrang();
            ViewBag.TinhTrang = items;
            return View(hoaDon);
        }

        // GET: HoaDons/Create
        public ActionResult Create()
        {
            List<SelectListItem> items = getTinhTrang();
            ViewBag.TinhTrang = items;
            ViewBag.NguoiLap = new SelectList(db.NhanViens, "ID", "HoTen");
            return View();
        }

        // POST: HoaDons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NgayLap,NgayThanhToan,NguoiLap,TongTien,HoTenKhachHang,SdtKhachHang,DiaChiKhachHang,TinhTrang")] HoaDon hoaDon)
        {
            SelectList nguoiLap = new SelectList(db.NhanViens, "ID", "HoTen", hoaDon.NguoiLap);
            List<SelectListItem> items = getTinhTrang();
            if (ModelState.IsValid)
            {
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.TinhTrang = items;
            ViewBag.NguoiLap = nguoiLap;
            return View(hoaDon);
        }

        [HttpPost]
        public dynamic TaoHoaDon(string jsondata)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            var object_ = js.Deserialize<dynamic>(jsondata);
            int tonggiatri = Int32.Parse(object_["tonggiatri"]);
            dynamic giohang = object_["giohang"];
            var khachhang = object_["khachhang"];
            HoaDon new_ = new HoaDon();
            new_.HoTenKhachHang = khachhang["hoten"];
            new_.SdtKhachHang = khachhang["sdt"];
            new_.DiaChiKhachHang = khachhang["diachi"];
            new_.NgayLap = DateTime.Now;
            new_.TinhTrang = 0;
            new_.TongTien = tonggiatri;
            try
            {
                HoaDonsController.hdcount += 1;
                int hoadon_count = db.HoaDons.Where(t => t.NgayLap.Value.Year == DateTime.Now.Year
                                                    && t.NgayLap.Value.Month == DateTime.Now.Month
                                                    && t.NgayLap.Value.Day == DateTime.Now.Day).Count();
                string hoadonid = "HD" + String.Join("", DateTime.Now.Date.ToShortDateString().Split('/')) + 
                                (hoadon_count+1).ToString("00.#");
                new_.ID = hoadonid;
                db.HoaDons.Add(new_);
                string ret = ThemHoaDon_TranSuc(hoadonid, giohang);
                if(ret != "1")
                {
                    return ret;
                }
                db.SaveChanges();
                IndexController.listgiohang.Clear();
                return 1;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public dynamic ThemHoaDon_TranSuc(string hdid, dynamic list_trangsuc)
        {
            foreach(dynamic item in list_trangsuc)
            {
                try
                {
                    db.SaveChanges();
                    db.SP_INSERT_HD_TRANGSUC(hdid, item["id"], Int32.Parse(item["soluong"]), item["tentrangsuc"], Int32.Parse(item["giathanh"]));
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return 1+"";
        }

        // GET: HoaDons/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> items = getTinhTrang();
            ViewBag.TinhTrang = items;
            ViewBag.NguoiLap = new SelectList(db.NhanViens, "ID", "HoTen", hoaDon.NguoiLap);
            return View(hoaDon);
        }

        // POST: HoaDons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NgayLap,NgayThanhToan,NguoiLap,TongTien,HoTenKhachHang,SdtKhachHang,DiaChiKhachHang,TinhTrang")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<SelectListItem> items = getTinhTrang();
            ViewBag.TinhTrang = items;
            ViewBag.NguoiLap = new SelectList(db.NhanViens, "ID", "HoTen", hoaDon.NguoiLap);
            return View(hoaDon);
        }

        // GET: HoaDons/Delete/5
        public ActionResult Delete(string id)
        {
            List<SelectListItem> items = getTinhTrang();
            ViewBag.TinhTrang = items;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            List<SelectListItem> items = getTinhTrang();
            ViewBag.TinhTrang = items;
            HoaDon hoaDon = db.HoaDons.Find(id);
            db.HoaDons.Remove(hoaDon);
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
