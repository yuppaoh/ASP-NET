using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanHang.EF;

namespace WebBanHang.Controllers.Backend
{
    public class ProductsController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();

        // GET: Products
        public ActionResult Index()
        {
            return View("~/Views/Backend/Products/Index.cshtml",db.products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View("~/Views/Backend/Products/Create.cshtml");
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include =
            "id,product_code,product_name,description,standard_cost,list_price,target_level,reorder_level," +
            "minimum_reorder_quantity,quantity_per_unit,discontinued,category,image")] product product,
            HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                //Xử lý file: lưu file vào thư mục UploadedFiles
                string _FileName = "";

                // di chuyển file vào thư mục mong muốn
                if (image != null && image.ContentLength > 0)
                {
                    _FileName = Path.GetFileName(image.FileName);
                    string _FileNameExtension = Path.GetExtension(image.FileName);
                    if ((_FileNameExtension == ".png" || _FileNameExtension == ".jpg"
                        || _FileNameExtension == ".jpeg" || _FileNameExtension == ".svg"
                        || _FileNameExtension == ".xlsx" || _FileNameExtension == ".docx"
                        ) == false)
                    {
                        return View(String.Format("File có đuôi {0} không được chấp nhận, vui lòng kiểm tra lại", _FileNameExtension));
                    }

                    string uploadFolderPath = Server.MapPath("~/UploadedFiles");
                    if (Directory.Exists(uploadFolderPath) == false)
                    {
                        Directory.CreateDirectory(uploadFolderPath);
                    }

                    string _Path = Path.Combine(uploadFolderPath, _FileName);
                    image.SaveAs(_Path);

                    product.image = image.FileName; //lưu tên file để sau này ghép lại với đường dấn, hiện ra trên trang index của product
                }

                //lưu dữ liệu
                
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.sanpham = product;
            return View("~/Views/Backend/Products/Edit.cshtml", product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,product_code,product_name,description,standard_cost," +
            "list_price,target_level,reorder_level,minimum_reorder_quantity,quantity_per_unit,discontinued," +
            "category,image")] product product, string image_oldFile, HttpPostedFileBase image) // HttpPost... để tạo biến image, sau này dùng ở dòng 125 - if (image == null)  
        {
            if (ModelState.IsValid)
            {
                string uploadFolderPath = Server.MapPath("~/UploadedFiles");

                if (image == null) // Nếu không cập nhật file (không chọn file)
                {
                    // Giữ nguyên ảnh củ
                    product.image = image_oldFile;
                }
                else // Nếu chọn file ảnh mới
                {
                    // 1. Xóa file ảnh củ
                    string filePathAnhCu = Path.Combine(uploadFolderPath, (product.image == null?"":product.image));
                    if (System.IO.File.Exists(filePathAnhCu))
                    {
                        System.IO.File.Delete(filePathAnhCu);
                    }

                    // 2. upload ảnh mới

                    string _FileName = "";

                    // di chuyển file vào thư mục mong muốn
                    if (image.ContentLength > 0)
                    {
                        _FileName = Path.GetFileName(image.FileName);
                        string _FileNameExtension = Path.GetExtension(image.FileName);
                        if ((_FileNameExtension == ".png" || _FileNameExtension == ".jpg"
                            || _FileNameExtension == ".jpeg" || _FileNameExtension == ".svg"
                            || _FileNameExtension == ".xlsx" || _FileNameExtension == ".docx"
                            ) == false)
                        {
                            return View(String.Format("File có đuôi {0} không được chấp nhận, vui lòng kiểm tra lại", _FileNameExtension));
                        }

                        
                        if (Directory.Exists(uploadFolderPath) == false)
                        {
                            Directory.CreateDirectory(uploadFolderPath);
                        }

                        string _Path = Path.Combine(uploadFolderPath, _FileName);
                        image.SaveAs(_Path);

                        // Lưu file vao database
                        product.image = _FileName;

                    }
                }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);  // Tìm dòng có id thứ mấy
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.sanpham = product;
            return View("~/Views/Backend/Products/Delete.cshtml", product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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
