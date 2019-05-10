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
    public class PhieuXuatHangsController : Controller
    {
        private TRANGSUCEntities db = new TRANGSUCEntities();

        // GET: PhieuXuatHangs
        public ActionResult Index()
        {
            var phieuXuatHangs = db.PhieuXuatHangs.Include(p => p.NhanVien);
            return View(phieuXuatHangs.ToList());
        }

        // GET: PhieuXuatHangs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuXuatHang phieuXuatHang = db.PhieuXuatHangs.Find(id);
            if (phieuXuatHang == null)
            {
                return HttpNotFound();
            }
            return View(phieuXuatHang);
        }

        // GET: PhieuXuatHangs/Create
        public ActionResult Create()
        {
            ViewBag.NguoiXuat = new SelectList(db.NhanViens, "ID", "HoTen");
            return View();
        }

        // POST: PhieuXuatHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoPhieu,TongGiaTri,NgayXuat,NguoiXuat")] PhieuXuatHang phieuXuatHang)
        {
            if (ModelState.IsValid)
            {
                db.PhieuXuatHangs.Add(phieuXuatHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NguoiXuat = new SelectList(db.NhanViens, "ID", "HoTen", phieuXuatHang.NguoiXuat);
            return View(phieuXuatHang);
        }

        // GET: PhieuXuatHangs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuXuatHang phieuXuatHang = db.PhieuXuatHangs.Find(id);
            if (phieuXuatHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.NguoiXuat = new SelectList(db.NhanViens, "ID", "HoTen", phieuXuatHang.NguoiXuat);
            return View(phieuXuatHang);
        }

        // POST: PhieuXuatHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoPhieu,TongGiaTri,NgayXuat,NguoiXuat")] PhieuXuatHang phieuXuatHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuXuatHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NguoiXuat = new SelectList(db.NhanViens, "ID", "HoTen", phieuXuatHang.NguoiXuat);
            return View(phieuXuatHang);
        }

        // GET: PhieuXuatHangs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuXuatHang phieuXuatHang = db.PhieuXuatHangs.Find(id);
            if (phieuXuatHang == null)
            {
                return HttpNotFound();
            }
            return View(phieuXuatHang);
        }

        // POST: PhieuXuatHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PhieuXuatHang phieuXuatHang = db.PhieuXuatHangs.Find(id);
            db.PhieuXuatHangs.Remove(phieuXuatHang);
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
