using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TrangSucSolution.Models;

namespace TrangSucSolution.Controllers
{
    public class LoginController : Controller
    {
        private TRANGSUCEntities db = new TRANGSUCEntities();

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Admin(NhanVien nvModel)
        {
            using (db)
            {
                if (nvModel.MatKhau == null)
                {
                    nvModel.MatKhau = "1";
                }
                nvModel.MatKhau = MD5Hash(nvModel.MatKhau);
                var NV = db.NhanViens.Where(x => x.ID == nvModel.ID && x.MatKhau == nvModel.MatKhau).FirstOrDefault();
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