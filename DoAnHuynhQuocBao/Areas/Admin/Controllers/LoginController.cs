using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnHuynhQuocBao.Models;
using DoAnHuynhQuocBao.common;

namespace DoAnHuynhQuocBao.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        DataHuynhQuocBaoEntities db = new DataHuynhQuocBaoEntities();

        
        public ActionResult changepass()
        {
            int id = ((UserLogin)Session["USER_SESSION"]).UserID;
            User us = db.User.FirstOrDefault(c => c.IDUser == id);
            return View(us);
        }

        [HttpPost]
        public ActionResult changepass(User us)
        {
            User user = db.User.FirstOrDefault(c => c.IDUser == us.IDUser);
            user.Name = us.Name;
            user.Pass = us.Pass;
            user.UserName = us.UserName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Admin/Login
        public ActionResult Index()
        {
            if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                ViewBag.username = Request.Cookies["username"].Value;
                ViewBag.password = Request.Cookies["password"].Value;
            }
            return View();
        }
        public void ghinhotaikhoan(string username, string password)
        {
            HttpCookie us = new HttpCookie("username");
            HttpCookie pas = new HttpCookie("password");

            us.Value = username;
            pas.Value = password;

            us.Expires = DateTime.Now.AddDays(1);
            pas.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(us);
            Response.Cookies.Add(pas);

        }
        [HttpPost]
        public ActionResult kiemtradulieu(string username, string password, string ghinho)
        {
            if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                username = Request.Cookies["username"].Value;
                password = Request.Cookies["password"].Value;
            }

            if (checkpassword(username, password))
            {
                var userSession = new UserLogin();
                //userSession.GroupID = 
                userSession.UserName = username;
                var listGroups = GetListGroupID(username);
                Session.Add("SESSION_GROUP", listGroups);
                Session.Add("USER_SESSION", userSession);
                var id = (from a in db.UserGroup
                          join b in db.User on a.ID equals b.GroupID
                          where b.UserName == username

                          select new
                          {
                              UserID = b.IDUser,
                              Username = b.UserName,
                              UserGroupID = b.GroupID,
                              UserGroupName = a.Name
                          });
                userSession.UserID = id.Single(c => c.Username == username).UserID;
                ghinhotaikhoan(username, password);

                if (listGroups.Contains("Admin"))
                {
                    return Redirect("~/Admin/TrangChu/Index");
                }
                else
                {
                    return Redirect("~/Home/Index");
                }

            }

            return Redirect("~/Admin/Login");
        }

        public List<string> GetListGroupID(string userName)
        {
            // var user = db.User.Single(x => x.UserName == userName);

            var data = (from a in db.UserGroup
                        join b in db.User on a.ID equals b.GroupID
                        where b.UserName == userName

                        select new
                        {
                            UserID = b.IDUser,
                            UserGroupID = b.GroupID,
                            UserGroupName = a.Name
                        });

            return data.Select(x => x.UserGroupName).ToList();

        }

        public bool checkpassword(string username, string password)
        {

            var ketqua = from u in db.User
                         where u.UserName == username && u.Pass == password
                         select u;
            if (ketqua.Count() > 0)
                return true;
            else
                return false;
        }
        //đăng suất
        public ActionResult SignOut()
        {
            Session["USER_SESSION"] = null;
            Session["SESSION_GROUP"] = null;
            if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                HttpCookie us = Request.Cookies["username"];
                HttpCookie ps = Request.Cookies["password"];

                ps.Expires = DateTime.Now.AddDays(-1);
                us.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(us);
                Response.Cookies.Add(ps);
            }
            return Redirect("/Admin/Login");
        }
    }
}