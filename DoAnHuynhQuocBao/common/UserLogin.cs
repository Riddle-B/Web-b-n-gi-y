using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnHuynhQuocBao.common
{
    public class UserLogin 
    {
        // GET: UserLogin
        public string UserName { set; get; }
        public int UserID { set; get; }


        public int GroupID { set; get; }
    }
}