using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.EF;

namespace WebBanHang.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult ProductDetail(int id)
        {
            product product = null;
            // Lấy dữ liệu danh sách sản phẩm
            using (WebBanHangEntities context = new WebBanHangEntities()) // tương đương câu lệch sql: select * from products
            {
                product = context.products.Where(p => p.id == id).FirstOrDefault();
            }

            // Truyền dữ liệu từ Controller -> View thông qua ViewBag
            ViewBag.SanPham = product;

            return View();
        }

        
    }
}