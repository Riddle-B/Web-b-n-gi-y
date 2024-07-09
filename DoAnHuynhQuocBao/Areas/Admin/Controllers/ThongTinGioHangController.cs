using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnHuynhQuocBao.Models;
using DoAnHuynhQuocBao.common;

namespace DoAnHuynhQuocBao.Areas.Admin.Controllers
{
    public class ThongTinGioHangController : Controller
    {
        DataHuynhQuocBaoEntities db = new DataHuynhQuocBaoEntities();
        //var id = (from a in db.UserGroup
        //          join b in db.User on a.ID equals b.GroupID
        //          where b.UserName == username

        //          select new
        //          {
        //              UserID = b.IDUser,
        //              Username = b.UserName,
        //              UserGroupID = b.GroupID,
        //              UserGroupName = a.Name
        //          });
        public ActionResult Index()
        {
            if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                ViewBag.usn = ((UserLogin)Session["USER_SESSION"]).UserName;
            }
            //var id = (from a in db.Order
            //          join b in db.OrderDetail on a.ID equals b.OrderID
            //          where b.UserName == username

            //          select new
            //          {
            //              ID = a.ID,
            //              NgayTao = a.CreatedDate,
            //              UserGroupID = b.GroupID,
            //              UserGroupName = a.Name
            //          });


            return View();
        }
    }
}