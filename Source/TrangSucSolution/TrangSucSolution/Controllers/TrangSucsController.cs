using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrangSucSolution.Models;

namespace TrangSucSolution.Controllers
{
    public class TrangSucsController : Controller
    {
        private TRANGSUCEntities db = new TRANGSUCEntities();
        public string filename;
        public static string hinhanh;

        // GET: TrangSucs
        public ActionResult Index()
        {
            var trangSucs = db.TrangSucs.Include(t => t.LoaiTrangSuc1);
            return View(trangSucs.ToList());
        }

        // GET: TrangSucs/Details/5
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

        // GET: TrangSucs/Create
        public ActionResult Create()
        {
            ViewBag.LoaiTrangSuc = new SelectList(db.LoaiTrangSucs, "ID", "TenLoai");
            return View();
        }

        // POST: TrangSucs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenTrangSuc,LoaiTrangSuc,GiaCong,KhoiLuongTinh,SoHat,GiaHat,HinhAnh,SoLuong")] TrangSuc trangSuc, HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    filename = Path.GetFileName(file.FileName);
                    string filepath = Path.Combine(Server.MapPath("~/Image"), filename);
                    file.SaveAs(filepath);
                }
            }
            if (ModelState.IsValid)
            {
                if(trangSuc.SoHat == null)
                {
                    trangSuc.SoHat = 0;
                }
                if (trangSuc.GiaHat == null)
                {
                    trangSuc.GiaHat = 0;
                }
                if (trangSuc.SoLuong == null)
                {
                    trangSuc.SoLuong = 0;
                }
                if (trangSuc.GiaCong == null)
                {
                    trangSuc.GiaCong = 0;
                }
                if (trangSuc.KhoiLuongTinh == null)
                {
                    trangSuc.KhoiLuongTinh = 0;
                }
                if (file != null)
                {
                    trangSuc.HinhAnh = filename;
                }
                else
                {
                    trangSuc.HinhAnh = hinhanh;
                }
                db.TrangSucs.Add(trangSuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoaiTrangSuc = new SelectList(db.LoaiTrangSucs, "ID", "TenLoai", trangSuc.LoaiTrangSuc);
            return View(trangSuc);
        }

        // GET: TrangSucs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrangSuc trangSuc = db.TrangSucs.Find(id);
            hinhanh = trangSuc.HinhAnh;
            if (trangSuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiTrangSuc = new SelectList(db.LoaiTrangSucs, "ID", "TenLoai", trangSuc.LoaiTrangSuc);
            return View(trangSuc);
        }

        

        // POST: TrangSucs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenTrangSuc,LoaiTrangSuc,GiaCong,SoLuong,KhoiLuongTinh,SoHat,GiaHat,HinhAnh")] TrangSuc trangSuc, HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    filename = Path.GetFileName(file.FileName);
                    string filepath = Path.Combine(Server.MapPath("~/Image"), filename);
                    file.SaveAs(filepath);
                }
            }
            
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    trangSuc.HinhAnh = filename;
                }
                else
                {
                    trangSuc.HinhAnh = hinhanh;
                }
                
                db.Entry(trangSuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiTrangSuc = new SelectList(db.LoaiTrangSucs, "ID", "TenLoai", trangSuc.LoaiTrangSuc);
            return View(trangSuc);
        }
        /*
        public ActionResult Edit(HttpPostedFileBase file)
        {
            try
            {
                if(file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string filepath = Path.Combine(Server.MapPath("~/Image"), filename);
                    file.SaveAs(filepath);
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        */

        // GET: TrangSucs/Delete/5
        public ActionResult Delete(string id)
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

        // POST: TrangSucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TrangSuc trangSuc = db.TrangSucs.Find(id);
            db.TrangSucs.Remove(trangSuc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: TrangSucs/Pdh
        public ActionResult Pdh()
        {
            return RedirectToAction("Create", "PhieuDatHangs");
        }

        public ActionResult Pxh()
        {
            return RedirectToAction("Create", "PhieuXuatHangs");
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
