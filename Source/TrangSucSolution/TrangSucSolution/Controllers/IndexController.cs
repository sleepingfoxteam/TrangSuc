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
    public class IndexController : Controller
    {
        private TRANGSUCEntities db = new TRANGSUCEntities();

        // GET: Index
        public ActionResult Index()
        {
            var trangSucs = db.TrangSucs.Include(t => t.LoaiTrangSuc1).OrderByDescending(ts => ts.ID);
            return View(trangSucs.ToList());
        }

        // GET: Index/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrangSuc trangSuc = db.TrangSucs.Find(id);
            if (trangSuc == null)
            {
                return HttpNotFound();
            }
            return View(trangSuc);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrangSuc trangSuc = db.TrangSucs.Find(id);
            if (trangSuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiTrangSuc = new SelectList(db.LoaiTrangSucs, "ID", "TenLoai", trangSuc.LoaiTrangSuc);
            return View(trangSuc);
        }

        // POST: Index/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenTrangSuc,LoaiTrangSuc,GiaCong,KhoiLuongTinh,SoHat,GiaHat,HinhAnh,SoLuong")] TrangSuc trangSuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trangSuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiTrangSuc = new SelectList(db.LoaiTrangSucs, "ID", "TenLoai", trangSuc.LoaiTrangSuc);
            return View(trangSuc);
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
