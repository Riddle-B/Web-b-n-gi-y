using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnHuynhQuocBao.Models
{
    public class RegisterModel
    {
        [Key]
        public long IDUser { set; get; }

        [Display(Name="Tên đăng nhập")]
        [Required(ErrorMessage ="Yêu cầu nhập tên đăng nhập")]
        public string UserName { set; get; }

        [Display(Name = "Mật khẩu")]
        [StringLength(20, MinimumLength = 6, ErrorMessage ="Độ dài mật khẩu ít nhất 6 ký tự")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string Pass { set; get; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Pass", ErrorMessage ="Xác nhận mật khẩu không đúng")]
        public string ConfirmPass { set; get; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Yêu cầu nhập họ và tên")]
        public string Name { set; get; }
    }
}