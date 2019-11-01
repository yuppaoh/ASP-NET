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


            //Route Contact
            // URL:/contact
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
