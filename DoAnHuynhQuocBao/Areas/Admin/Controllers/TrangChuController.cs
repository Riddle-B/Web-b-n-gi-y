using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnHuynhQuocBao.Models;
using DoAnHuynhQuocBao.common;

namespace DoAnHuynhQuocBao.Areas.Admin.Controllers
{
    public class TrangChuController : Controller
    {
        DataHuynhQuocBaoEntities db = new DataHuynhQuocBaoEntities();

        public ActionResult Index()
        {
            if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                ViewBag.usn = ((UserLogin)Session["USER_SESSION"]).UserName;
            }
            var dssp = from sp in db.SanPham select sp;
            var dsloaisp = from l in db.LoaiSP select l;
            ViewBag.danhsachsanpham = dssp;
            ViewBag.danhsachloaisanpham = dsloaisp;
            return View();
        }
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult Create(SanPham sp, HttpPostedFileBase hinh)
        {



            db.SanPham.Add(sp);

            db.SaveChanges();

            if (hinh != null && hinh.ContentLength > 0)
            {
                int id = int.Parse(db.SanPham.ToList().Last().ID.ToString());

                string _FileName = "";
                int index = hinh.FileName.IndexOf('.');
                _FileName = "sp" + id.ToString() + "." + hinh.FileName.Substring(index + 1);

                string _path = Path.Combine(Server.MapPath("~/Upload"), _FileName);
                hinh.SaveAs(_path);

                SanPham usp = db.SanPham.FirstOrDefault(x => x.ID == id);
                usp.Hinh = _FileName;
                db.SaveChanges();
            }


            return RedirectToAction("Index");
        }
        public void SetViewBag()
        {
            ViewBag.IDLoai = db.LoaiSP.ToList();
        }
    }
}