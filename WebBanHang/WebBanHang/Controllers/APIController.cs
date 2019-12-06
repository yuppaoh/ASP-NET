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



        // POST: http://domain.com/api/products/{id}/delete
        [System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteProductByApi(int id)
        {
            try
            {
                using (WebBanHangEntities context = new WebBanHangEntities())
                {

                    product product = context.products.Find(id);
                    context.products.Remove(product);
                    context.SaveChanges();
                    
                }
                object result = new
                {
                    Code = 200,
                    Message = "Da xoa thanh cong!"
                };
                return Json(result);

            }
            catch (Exception e)
            {
                object result = new
                {
                    Code = 500,
                    Message = "Da co loi xay ra " + e.Message
                };
                return Json(result);
            }
        }


        // POST: http://domain.com/api/products/create
        [System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateProductByApi([FromBody] product product)
        {
            try
            {
                using (WebBanHangEntities context = new WebBanHangEntities())
                {

                    //product product = context.products.Find(id);
                    context.products.Add(product);
                    context.SaveChanges();

                }
                object result = new
                {
                    Code = 201,
                    Message = "Da tao thanh cong!",
                    CreatedObject = product
                };
                return Json(result);

            }
            catch (Exception e)
            {
                object result = new
                {
                    Code = 501,
                    Message = "Da co loi xay ra " + e.Message
                };
                return Json(result);
            }
        }

        // POST: http://domain.com/api/products/{id}/edit
        [System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditeProductByApi(int id, [FromBody] product product)
        {
            try
            {
                using (WebBanHangEntities context = new WebBanHangEntities())
                {

                    product productEdit = context.products.Find(id);
                    if (String.IsNullOrEmpty(product.product_code))
                    {
                        productEdit.product_code = product.product_code;
                    }
                    if (String.IsNullOrEmpty(product.product_name))
                    {
                        productEdit.product_name = product.product_name;
                    }
                    if (String.IsNullOrEmpty(product.description))
                    {
                        productEdit.description = product.description;
                    }
                    if (product.standard_cost>0)
                    {
                        productEdit.standard_cost = product.standard_cost;
                    }
                    if (product.list_price > 0)
                    {
                        productEdit.list_price = product.list_price;
                    }
                    if (product.target_level > 0)
                    {
                        productEdit.target_level = product.target_level;
                    }
                    if (product.minimum_reorder_quantity > 0)
                    {
                        productEdit.minimum_reorder_quantity = product.minimum_reorder_quantity;
                    }
                    if (String.IsNullOrEmpty(product.quantity_per_unit))
                    {
                        productEdit.quantity_per_unit = product.quantity_per_unit;
                    }
                    if (product.discontinued > 0)
                    {
                        productEdit.discontinued = product.discontinued;
                    }
                    if (String.IsNullOrEmpty(product.category))
                    {
                        productEdit.category = product.category;
                    }
                    if (String.IsNullOrEmpty(product.image))
                    {
                        productEdit.image = product.image;
                    }

                        
                    context.SaveChanges();
                    
                 
                }
                object result = new
                {
                    Code = 202,
                    Message = "Da thay doi thong tin thanh cong!"
                };
                return Json(result);

            }
            catch (Exception e)
            {
                object result = new
                {
                    Code = 502,
                    Message = "Da co loi xay ra " + e.Message
                };
                return Json(result);
            }
        }




    }



}
