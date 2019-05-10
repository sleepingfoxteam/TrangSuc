using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrangSucSolution.Models;

namespace TrangSucSolution.Controllers
{
    public class HoaDonsController : Controller
    {
        private TRANGSUCEntities db = new TRANGSUCEntities();

        public List<SelectListItem> getTinhTrang()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Chờ thanh toán", Value = "0" });
            items.Add(new SelectListItem { Text = "Đã thanh toán", Value = "1" });
            items.Add(new SelectListItem { Text = "Chờ xác nhận", Value = "2" });
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
