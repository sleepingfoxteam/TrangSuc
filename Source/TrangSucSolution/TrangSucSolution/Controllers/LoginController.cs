using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrangSucSolution.Models;

namespace TrangSucSolution.Controllers
{
    public class LoginController : Controller
    {
        private TRANGSUCEntities db = new TRANGSUCEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Admin(TrangSucSolution.Models.NhanVien nvModel)
        {
            using (db)
            {
                var NV = db.NhanViens.Where(x => x.ID == nvModel.ID).FirstOrDefault();
                if(NV == null)
                {
                    nvModel.LoginErroMessage = "Wrong username or password!";
                    return View("Index", nvModel);
                }
                else
                {
                    Session["NhanVienID"] = nvModel.ID;
                    return RedirectToAction("Index", "TrangSucs");
                }
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}