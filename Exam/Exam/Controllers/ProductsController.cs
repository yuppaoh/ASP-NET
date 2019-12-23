using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Exam.EF;

namespace Exam.Controllers
{
    public class ProductsController : Controller
    {
        private WADEntities db = new WADEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.cat_id = new SelectList(db.Categories, "Cat_ID", "Cat_Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pro_id,pro_name,quantity,pro_price,pro_image,cat_id,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cat_id = new SelectList(db.Categories, "Cat_ID", "Cat_Name", product.cat_id);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            //ViewBag.cat_id = new SelectList(db.Categories, "Cat_ID", "Cat_Name", product.cat_id);
            ViewBag.sanpham = product; // Bo sung
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pro_id,pro_name,quantity,pro_price,pro_image,cat_id," +
            "Description")] Product product, string image_oldFile, HttpPostedFileBase pro_image)
        {
            if (ModelState.IsValid)
            {
                // --------- Bo sung 1 Start ---------
                string uploadFolderPath = Server.MapPath("~/UploadFile");

                if (pro_image == null) // Nếu không cập nhật ảnh mới (không chọn file)
                {
                    product.pro_image = image_oldFile;  // Giữ nguyên ảnh củ
                }
                else // Nếu chọn file ảnh mới để cập nhật ảnh
                {
                    // 1. Xóa file ảnh củ
                    string filePathAnhCu = Path.Combine(uploadFolderPath, (product.pro_image == null ? "" : product.pro_image));
                    if (System.IO.File.Exists(filePathAnhCu))
                    {
                        System.IO.File.Delete(filePathAnhCu);
                    }

                    // 2. upload ảnh mới

                    string _FileName = "";

                    // di chuyển file vào thư mục mong muốn
                    if (pro_image.ContentLength > 0)
                    {
                        _FileName = Path.GetFileName(pro_image.FileName);
                        string _FileNameExtension = Path.GetExtension(pro_image.FileName);
                        if ((_FileNameExtension == ".png" || _FileNameExtension == ".jpg"
                            || _FileNameExtension == ".jpeg" || _FileNameExtension == ".svg"
                            ) == false)
                        {
                            return View(String.Format("File extension {0} is not acepted, please check agian!", _FileNameExtension));
                        }


                        if (Directory.Exists(uploadFolderPath) == false)
                        {
                            Directory.CreateDirectory(uploadFolderPath);
                        }

                        string _Path = Path.Combine(uploadFolderPath, _FileName);
                        pro_image.SaveAs(_Path);

                        // Lưu file vao database
                        product.pro_image = _FileName;

                    }
                }



                // --------- Bo sung1  End ---------

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            ViewBag.cat_id = new SelectList(db.Categories, "Cat_ID", "Cat_Name", product.cat_id);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
