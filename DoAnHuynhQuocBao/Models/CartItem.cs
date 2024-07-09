using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnHuynhQuocBao.Models
{
    [Serializable]
    public class CartItem
    {

        public SanPham product { set; get; }
        public int Quantity { set; get; }

    }
}