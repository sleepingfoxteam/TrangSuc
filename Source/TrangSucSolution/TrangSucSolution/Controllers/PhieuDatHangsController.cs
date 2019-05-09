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
    public class PhieuDatHangsController : Controller
    {
        private TRANGSUCEntities db = new TRANGSUCEntities();

        // GET: PhieuDatHangs
        public ActionResult Index()
        {
            var phieuDatHangs = db.PhieuDatHangs.Include(p => p.NhanVien).Include(p => p.TrangSuc);
            return View(phieuDatHangs.ToList());
        }

        // GET: PhieuDatHangs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDatHang phieuDatHang = db.PhieuDatHangs.Find(id);
            if (phieuDatHang == null)
            {
                return HttpNotFound();
            }
            return View(phieuDatHang);
        }

        // GET: PhieuDatHangs/Create
        public ActionResult Create()
        {
            ViewBag.NguoiLap = new SelectList(db.NhanViens, "ID", "HoTen");
            ViewBag.MaMatHang = new SelectList(db.TrangSucs, "ID", "ID");
            return View();
        }

        // POST: PhieuDatHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoPhieu,MaMatHang,TenMatHang,SoLuong,DonGia,TongGiaTri,NgayLap,NguoiLap")] PhieuDatHang phieuDatHang)
        {
            if (ModelState.IsValid)
            {
                db.PhieuDatHangs.Add(phieuDatHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NguoiLap = new SelectList(db.NhanViens, "ID", "HoTen", phieuDatHang.NguoiLap);
            ViewBag.MaMatHang = new SelectList(db.TrangSucs, "ID", "ID", phieuDatHang.MaMatHang);
            return View(phieuDatHang);
        }

        // GET: PhieuDatHangs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDatHang phieuDatHang = db.PhieuDatHangs.Find(id);
            if (phieuDatHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.NguoiLap = new SelectList(db.NhanViens, "ID", "HoTen", phieuDatHang.NguoiLap);
            ViewBag.MaMatHang = new SelectList(db.TrangSucs, "ID", "ID", phieuDatHang.MaMatHang);
            return View(phieuDatHang);
        }

        // POST: PhieuDatHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoPhieu,MaMatHang,TenMatHang,SoLuong,DonGia,TongGiaTri,NgayLap,NguoiLap")] PhieuDatHang phieuDatHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuDatHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NguoiLap = new SelectList(db.NhanViens, "ID", "HoTen", phieuDatHang.NguoiLap);
            ViewBag.MaMatHang = new SelectList(db.TrangSucs, "ID", "ID", phieuDatHang.MaMatHang);
            return View(phieuDatHang);
        }

        // GET: PhieuDatHangs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuDatHang phieuDatHang = db.PhieuDatHangs.Find(id);
            if (phieuDatHang == null)
            {
                return HttpNotFound();
            }
            return View(phieuDatHang);
        }

        // POST: PhieuDatHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PhieuDatHang phieuDatHang = db.PhieuDatHangs.Find(id);
            db.PhieuDatHangs.Remove(phieuDatHang);
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
