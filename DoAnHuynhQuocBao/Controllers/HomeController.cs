using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnHuynhQuocBao.Models;
using DoAnHuynhQuocBao.common;
namespace DoAnHuynhQuocBao.Controllers
{
    public class HomeController : Controller
    {      
        DataHuynhQuocBaoEntities db = new DataHuynhQuocBaoEntities();
        List<SanPham> spsp = new List<SanPham>();
        public ActionResult Index(string SearchString = null, string idlsp = null)
        {
            ViewBag.lstsp = db.getloaisp().ToList();
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }

            if (string.IsNullOrEmpty(SearchString))
            {
                spsp = db.SanPham.ToList();
            }
            else
            {
                spsp = db.SanPham.Where(p => p.TenSP.Contains(SearchString)).ToList();
            }
            int id = Convert.ToInt32(idlsp);
            if (idlsp != null)
            {
                spsp = db.SanPham.Where(c => c.IDLoai == id).ToList();
            }

            return View(spsp);
        }
    }

}