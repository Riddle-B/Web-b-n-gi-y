using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnHuynhQuocBao.Models;
using DoAnHuynhQuocBao.common;
namespace DoAnHuynhQuocBao.Controllers
{
    public class NuController : Controller
    {
        // GET: Nu
        DataHuynhQuocBaoEntities db = new DataHuynhQuocBaoEntities();
        List<SanPham> spsp = new List<SanPham>();
        public ActionResult Index()
        {
            if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                ViewBag.usn = ((UserLogin)Session["USER_SESSION"]).UserName;
            }
            var sp = db.SanPham.Where(x => x.IDLoai == 2);
            ViewBag.splist = sp.ToList();
            return View();
        }
    }
}