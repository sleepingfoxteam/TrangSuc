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
    public class PhieuXuatHangsController : Controller
    {
        private TRANGSUCEntities db = new TRANGSUCEntities();

        public static dynamic list_trangsuc = null;

        public static string idphieu = "";

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
            ViewBag.tongtien = 0;
            ViewBag.ngayxuat = DateTime.Now.Date;
            int phieu_count = db.PhieuXuatHangs.Where(t => t.NgayXuat.Value.Year == DateTime.Now.Year &&
                                                t.NgayXuat.Value.Month == DateTime.Now.Month &&
                                                t.NgayXuat.Value.Day == DateTime.Now.Day).Count();
            string idphieu = "XH" + String.Join("", DateTime.Now.Date.ToShortDateString().Split('/')) + (phieu_count + 1).ToString("00.#");
            ViewBag.idphieu = idphieu;
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

        public ActionResult HoanThanhChon(string jsondata)
        {
            ViewBag.NguoiXuat = new SelectList(db.NhanViens, "ID", "HoTen");
            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic object_ = js.Deserialize<dynamic>(jsondata);
            PhieuXuatHangsController.list_trangsuc = object_["trangsucs"];
            ViewBag.ngayxuat = DateTime.Now.Date;
            ViewBag.tongtien = object_["tongtien"];
            int phieu_count = db.PhieuXuatHangs.Where(t => t.NgayXuat.Value.Year == DateTime.Now.Year &&
                                                t.NgayXuat.Value.Month == DateTime.Now.Month &&
                                                t.NgayXuat.Value.Day == DateTime.Now.Day).Count();
            string idphieu = "XH" + String.Join("", DateTime.Now.Date.ToShortDateString().Split('/')) + (phieu_count + 1).ToString("00.#");
            ViewBag.idphieu = idphieu;
            PhieuXuatHangsController.idphieu = idphieu;
            return View("Create");
        }

        // POST: PhieuXuatHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HoanThanhChon([Bind(Include = "SoPhieu,TongGiaTri,NgayXuat,NguoiXuat")] PhieuXuatHang phieuXuatHang)
        {
            if (ModelState.IsValid)
            {
                db.PhieuXuatHangs.Add(phieuXuatHang);
                foreach (var item in PhieuXuatHangsController.list_trangsuc)
                {
                    db.SaveChanges();
                    db.SP_INSERT_PXH_TRANGSUC(PhieuXuatHangsController.idphieu, item["mats"], item["tents"]
                        , Int32.Parse(item["soluong"]), Int32.Parse(item["giathanh"]));
                    db.SaveChanges();
                }
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

        public ActionResult ThemSanPhamVaoPhieu()
        {
            var trangsuc = db.TrangSucs.Where(t => t.ID != null);
            ViewBag.giathitruong = IndexController.giathitruong;
            return View(trangsuc.ToList());
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
