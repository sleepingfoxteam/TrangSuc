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
    public class PhieuDatHangsController : Controller
    {
        private TRANGSUCEntities db = new TRANGSUCEntities();

        public static dynamic list_trangsuc = null;

        public static string idphieu = "";

        // GET: PhieuDatHangs
        public ActionResult Index()
        {
            var phieuDatHangs = db.PhieuDatHangs.Include(p => p.NhanVien);
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
            ViewBag.tongtien = 0;
            ViewBag.ngaylap = DateTime.Now.Date;
            int phieu_count = db.PhieuDatHangs.Where(t => t.NgayLap.Value.Year == DateTime.Now.Year &&
                                                t.NgayLap.Value.Month == DateTime.Now.Month &&
                                                t.NgayLap.Value.Day == DateTime.Now.Day).Count();
            string idphieu = "DH" + String.Join("", DateTime.Now.Date.ToShortDateString().Split('/')) + (phieu_count+1).ToString("00.#");
            ViewBag.idphieu = idphieu;
            return View();
        }

        // POST: PhieuDatHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoPhieu,TongGiaTri,NgayLap,NguoiLap")] PhieuDatHang phieuDatHang)
        {
            if (ModelState.IsValid)
            {
                db.PhieuDatHangs.Add(phieuDatHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NguoiLap = new SelectList(db.NhanViens, "ID", "HoTen", phieuDatHang.NguoiLap);

            return View(phieuDatHang);
        }

        public dynamic addPhieu_TrangSuc()
        {
            return -1;
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
            return View(phieuDatHang);
        }

        // POST: PhieuDatHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoPhieu,TongGiaTri,NgayLap,NguoiLap")] PhieuDatHang phieuDatHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuDatHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NguoiLap = new SelectList(db.NhanViens, "ID", "HoTen", phieuDatHang.NguoiLap);
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
        
        public ActionResult ThemSanPhamVaoPhieu()
        {
            var trangsuc = db.TrangSucs.Where(t => t.ID != null);
            ViewBag.giathitruong = IndexController.giathitruong;
            return View(trangsuc.ToList());
        } 

        public ActionResult HoanThanhChon(string jsondata)
        {
            ViewBag.NguoiLap = new SelectList(db.NhanViens, "ID", "HoTen");
            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic object_ = js.Deserialize<dynamic>(jsondata);
            PhieuDatHangsController.list_trangsuc = object_["trangsucs"];
            ViewBag.ngaylap = DateTime.Now.Date;
            ViewBag.tongtien = object_["tongtien"];
            int phieu_count = db.PhieuDatHangs.Where(t => t.NgayLap.Value.Year == DateTime.Now.Year &&
                                                t.NgayLap.Value.Month == DateTime.Now.Month &&
                                                t.NgayLap.Value.Day == DateTime.Now.Day).Count();
            string idphieu = "DH" + String.Join("", DateTime.Now.Date.ToShortDateString().Split('/')) + (phieu_count + 1).ToString("00.#");
            ViewBag.idphieu = idphieu;
            PhieuDatHangsController.idphieu = idphieu;
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HoanThanhChon([Bind(Include = "SoPhieu,TongGiaTri,NgayLap,NguoiLap")] PhieuDatHang phieuDatHang)
        {
            if (ModelState.IsValid)
            {
                db.PhieuDatHangs.Add(phieuDatHang);
                foreach(var item in PhieuDatHangsController.list_trangsuc)
                {
                    db.SaveChanges();
                    db.SP_INSERT_PDH_TRANGSUC(PhieuDatHangsController.idphieu, item["mats"], item["tents"]
                        , Int32.Parse(item["soluong"]), Int32.Parse(item["gia"]));
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.NguoiLap = new SelectList(db.NhanViens, "ID", "HoTen", phieuDatHang.NguoiLap);

            return View(phieuDatHang);
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
