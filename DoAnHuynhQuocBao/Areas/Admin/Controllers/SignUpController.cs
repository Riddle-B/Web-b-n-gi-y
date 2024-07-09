using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnHuynhQuocBao.Models;
using DoAnHuynhQuocBao.common;

namespace DoAnHuynhQuocBao.Areas.Admin.Controllers
{
    public class SignUpController : Controller
    {
        DataHuynhQuocBaoEntities db = new DataHuynhQuocBaoEntities();
        List<User> lstct = new List<User>();
        public ActionResult Index()
        {
            if (TempData["existingCustomer"] != null)
            {
                ViewBag.Success = TempData["existingCustomer"];
            }
            else
            {
                ViewBag.Success = TempData["existingCustomer"];

            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(User us,string ConfirmPass)
        {
            if(us.Pass == ConfirmPass)
            {
                var existingCustomer = db.User.FirstOrDefault(x => x.UserName == us.UserName);
                if (existingCustomer == null)
                {
                    us.GroupID = 2;
                    TempData["existingCustomer"] = "Đăng Ký Thành Công";
                    db.User.Add(us);
                    db.SaveChanges();
                    return Redirect("~/Admin/Login");

                }
                else
                {
                    TempData["existingCustomer"] = "tên tài khoản đã tồn tại";
                }
            }
            else
            {
                TempData["existingCustomer"] = "Lặp lại mật khẩu không đúng";
            }
            return Redirect("~/Admin/SignUp");
        }
    }
}