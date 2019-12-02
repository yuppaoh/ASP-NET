using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebBanHang.EF;

namespace WebBanHang.Controllers
{
    public class APIController : Controller
    {
        public System.Web.Mvc.ActionResult GetProduct()
        {
            dynamic lstProduct = null;
            using (WebBanHangEntities context = new WebBanHangEntities())
            {
                //lstProduct = context.products.ToList();
                lstProduct = (from p in context.products
                              select new
                              {
                                  p.id,
                                  p.product_code,
                                  p.product_name,
                                  p.standard_cost,
                                  p.list_price
                              }).ToList();
            }

            return Json(lstProduct, JsonRequestBehavior.AllowGet);
        }


        public System.Web.Mvc.ActionResult GetCustomers()
        {
            dynamic lstCustomer = null;
            using (WebBanHangEntities context = new WebBanHangEntities())
            {
                //lstProduct = context.products.ToList();
                lstCustomer = (from p in context.customers
                              select new
                              {
                                  p.id,
                                  p.first_name,
                                  p.last_name,
                                  p.address1,
                                  p.address2,
                                  p.email,
                                  p.phone
                              }).ToList();
            }

            return Json(lstCustomer, JsonRequestBehavior.AllowGet);
        }

        public System.Web.Mvc.ActionResult GetOrders()
        {
            dynamic lstOrder = null;
            using (WebBanHangEntities context = new WebBanHangEntities())
            {
                //lstProduct = context.products.ToList();
                lstOrder = (from o in context.orders join c in context.customers on o.customer_id equals c.id
                               select new
                               {
                                   orderid = o.id,
                                   customerid = c.id,
                                   c.first_name,
                                   c.last_name,
                                   c.address1,
                                   c.address2,
                                   c.email,
                                   c.phone
                               }).ToList();
            }

            return Json(lstOrder, JsonRequestBehavior.AllowGet);
        }

    }
    
}
