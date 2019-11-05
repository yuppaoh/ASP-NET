using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Controllers
{
    public class PageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SanPham()
        {
            ViewBag.Message = "Trang San pham";

            return View();
        }

        public ActionResult GioiThieu()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult LienHe()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DangNhap()
        {
            ViewBag.Message = "Trang dang nhap.";

            return View();
        }

        public ActionResult DangKy()
        {
            ViewBag.Message = "Trang dang nhap.";

            return View();
        }

        public ActionResult ChinhSach()
        {
            ViewBag.Message = "Chinh sach ban hang";

            return View();
        }

        public ActionResult DieuKhoan()
        {
            ViewBag.Message = "Dieu khoan";

            return View();
        }
    }
}