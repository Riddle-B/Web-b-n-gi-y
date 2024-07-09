using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnHuynhQuocBao.Models;
using System.IO;
using DoAnHuynhQuocBao.common;

namespace DoAnHuynhQuocBao.Areas.Admin.Controllers
{
    public class KidAController : Controller
    {
        // GET: Admin/KidA
        DataHuynhQuocBaoEntities db = new DataHuynhQuocBaoEntities();
        List<SanPham> spsp = new List<SanPham>();

        public ActionResult Index()
        {
            if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                ViewBag.usn = ((UserLogin)Session["USER_SESSION"]).UserName;
            }
            var sp = db.SanPham.Where(x => x.IDLoai == 3);
            ViewBag.splist = sp.ToList();
            return View();
        }

        public ActionResult Edit(int id)

        {

            SanPham sp = db.SanPham.FirstOrDefault(c => c.ID == id);
            SetViewBag(sp.IDLoai);
            return View(sp);
        }
        [HttpPost]
        public ActionResult Edit(SanPham sp, HttpPostedFileBase hinh)
        {
            SanPham usp = db.SanPham.FirstOrDefault(c => c.ID == sp.ID);

            usp.TenSP = sp.TenSP;
            usp.Gia = sp.Gia;
            usp.IDLoai = sp.LoaiSP.IDLoai;


            if (hinh != null && hinh.ContentLength > 0)
            {
                int id = sp.ID;

                string _FileName = "";
                int index = hinh.FileName.IndexOf('.');
                _FileName = "sp" + id.ToString() + "." + hinh.FileName.Substring(index + 1);
                string _path = Path.Combine(Server.MapPath("~/Upload"), _FileName);
                hinh.SaveAs(_path);
                usp.Hinh = _FileName;

            }
            db.SaveChanges();


            return RedirectToAction("index");
        }
        public ActionResult Delete(int id)
        {
            SanPham sp = db.SanPham.FirstOrDefault(c => c.ID == id);
            db.SanPham.Remove(sp);
            db.SaveChanges();

            return RedirectToAction("index");
        }
        public void SetViewBag(int? selectedid = null)
        {
            ViewBag.IDLoai = new SelectList(db.LoaiSP.ToList(), "IDLoai", "TenLoai", selectedid);
        }
    }
}