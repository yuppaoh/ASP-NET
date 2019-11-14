using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.EF;

namespace WebBanHang.Controllers
{
    public class PageController : Controller
    {
        public ActionResult Index()
        {
            List<product> lstProduct = new List<product>();

            // Lấy dữ liệu danh sách sản phẩm
            using (WebBanHangEntities context = new WebBanHangEntities()) // tương đương câu lệch sql: select * from products
            {
                lstProduct = context.products.ToList();
            }

            // Truyền dữ liệu từ Controller -> View thông qua ViewBag
            ViewBag.DanhSachSanPham = lstProduct;

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