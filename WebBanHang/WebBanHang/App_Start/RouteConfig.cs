using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebBanHang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Frontend Default
            // ----------------------------------------------------------------------------

            //Route Contact
            // URL:/contact

            routes.MapRoute(
               name: "page.trang-chu",
               url: "",
               defaults: new { controller = "Page", action = "Index" }
           );

           routes.MapRoute(
               name: "page.san-pham",
               url: "san-pham",
               defaults: new { controller = "Page", action = "SanPham" }
           );

           routes.MapRoute(
               name: "page.gioi-thieu",
               url: "gioi-thieu",
               defaults: new { controller = "Page", action = "GioiThieu" }
           );

           routes.MapRoute(
               name: "page.lien-he",
               url: "lien-he",
               defaults: new { controller = "Page", action = "LienHe" }
           );

           routes.MapRoute(
               name: "page.dang-nhap",
               url: "dang-nhap",
               defaults: new { controller = "Page", action = "DangNhap" }
           );

           routes.MapRoute(
               name: "page.dang-ky",
               url: "dang-ky",
               defaults: new { controller = "Page", action = "DangKy" }
           );

           routes.MapRoute(
               name: "page.chinh-sach",
               url: "chinh-sach",
               defaults: new { controller = "Page", action = "ChinhSach" }
           );

           routes.MapRoute(
              name: "page.dieu-khoan",
              url: "dieu-khoan",
              defaults: new { controller = "Page", action = "DieuKhoan" }
           );

            routes.MapRoute(
              name: "product.detail",
              url: "product-detail",
              defaults: new { controller = "Product", action = "ProductDetail" }
           );

            // BackEnd
            // ----------------------------------------------------------------------------
            routes.MapRoute(
              name: "admin.page.dashboard",
              url: "admin/dashboard",
              defaults: new { controller = "Dashboard", action = "Index" },
              namespaces: new string[] {"WebBanHang.Controller.Backend"}
            );

            // products/index
            routes.MapRoute(
              name: "admin.products.index",
              url: "admin/products",
              defaults: new { controller = "Products", action = "Index" },
              namespaces: new string[] { "WebBanHang.Controller.Backend" }
            );

            // product/detail
            routes.MapRoute(
              name: "admin.products.detail",
              url: "admin/products/detail",
              defaults: new { controller = "Products", action = "Detail" },
              namespaces: new string[] { "WebBanHang.Controller.Backend" }
            );

            // route Them moi san pham
            // URL: /Admin/products/create
            routes.MapRoute(
              name: "admin.products.create",
              url: "admin/products/create",
              defaults: new { controller = "Products", action = "Create" },
              namespaces: new string[] { "WebBanHang.Controller.Backend" }
            );

            // Sửa product - edit
            routes.MapRoute(
              name: "admin.products.edit",
              url: "admin/products/edit/{id}",
              defaults: new { controller = "Products", action = "Edit" },
              namespaces: new string[] { "WebBanHang.Controller.Backend" }
            );

            // Xóa product - delete
            routes.MapRoute(
              name: "admin.products.delete",
              url: "admin/products/delete/{id}",
              defaults: new { controller = "Products", action = "Delete" },
              namespaces: new string[] { "WebBanHang.Controller.Backend" }
            );

            // API
            // ----------------------------------------------------------------------------

            // API product
            routes.MapRoute(
              name: "api.products",
              url: "api/products",
              defaults: new { controller = "Api", action = "GetProduct" }
            );

            // API customer
            routes.MapRoute(
              name: "api.customers",
              url: "api/customers",
              defaults: new { controller = "Api", action = "GetCustomers" }
            );

            // API customer
            routes.MapRoute(
              name: "api.orders",
              url: "api/orders",
              defaults: new { controller = "Api", action = "GetOrders" }
            );

            // Delete product by API
            routes.MapRoute(
              name: "api.products.delete",
              url: "api/products/{id}/delete",
              defaults: new { controller = "Api", action = "DeleteProductByApi", id = UrlParameter.Optional }
            );

            // Create product by API
            routes.MapRoute(
              name: "api.products.create",
              url: "api/products/create",
              defaults: new { controller = "Api", action = "CreateProductByApi", id = UrlParameter.Optional }
            );

            // Edit product by API
            routes.MapRoute(
              name: "api.products.edit",
              url: "api/products/{id}/edit",
              defaults: new { controller = "Api", action = "EditeProductByApi", id = UrlParameter.Optional }
            );



            // Frontend Default
            // ----------------------------------------------------------------------------
            //Route mat dinh cua trang web
            // URL:/
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Page", action = "Index", id = UrlParameter.Optional }
            );
        }       
    }
}
