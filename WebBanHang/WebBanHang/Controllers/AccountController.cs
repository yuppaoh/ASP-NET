using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.EF;

namespace WebBanHang.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string XuLyDangNhap(string email, string password)
        {
            using (WebBanHangEntities context = new WebBanHangEntities())
            {
                // Cach 1: Viet theo dang Method
                var objEmployeeLogin = context.employees
                    .Where(p => p.email == email && p.password == password)
                    .FirstOrDefault();

                // Cach 2: Viet theo kieu LINQ
                //var objEmployeeLogin2 = (from p in context.employees
                //                         where p.email == email
                //                         && p.password == password
                //                         select p).FirstOrDefault();


                if (objEmployeeLogin == null)
                {
                    return "Khong hop le";
                }
                else
                {
                    return String.Format("Xin chao anh {0} {1}", objEmployeeLogin.last_name, objEmployeeLogin.first_name);
                }
            }
            
        }




        [HttpPost]
        public string XuLyDangKy(string last_name, string first_name, string email
            , string password, HttpPostedFileBase avatar, string job_title, string department, int manager_id,
            string phone, string address1, string address2, string city, string state, string postal_code, string country)
        {
            try
            {
                string _FileName = "";
                string datetimeFolderName = "";

                if (avatar.ContentLength>0)
                {
                    _FileName = Path.GetFileName(avatar.FileName);
                    string _FileNameExtension = Path.GetExtension(avatar.FileName);
                    if((_FileNameExtension == ".png" || _FileNameExtension == ".jpg"
                        || _FileNameExtension == ".jpeg" || _FileNameExtension == ".svg"
                        || _FileNameExtension == ".xlsx" || _FileNameExtension == ".docx"
                        ) == false)
                    {
                        return String.Format("File co duoi {0} khong duoc chap nhan, vui long kiem tra lai", _FileNameExtension);
                    }

                    DateTime now = DateTime.Now;
                    datetimeFolderName = String.Format("{0}{1}{2}{3}{4}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                    string uploadFolderPath = Server.MapPath("~/UploadedFiles/" + datetimeFolderName);
                    if (Directory.Exists(uploadFolderPath) == false)
                    {
                        Directory.CreateDirectory(uploadFolderPath);
                    }

                    string _Path = Path.Combine(uploadFolderPath, _FileName);
                    avatar.SaveAs(_Path);
                }
                using (WebBanHangEntities context = new WebBanHangEntities())
                {
                    //Tao 1 dong moi nhung chua luu vao table cua Database
                    employee newRow = new employee();
                    newRow.last_name = last_name;
                    newRow.first_name = first_name;
                    newRow.email = email;
                    newRow.password = password;
                    //newRow.avatar = avatar;
                    newRow.avatar = datetimeFolderName + "/" + _FileName;
                    newRow.job_title = job_title;
                    newRow.department = department;
                    newRow.manager_id = manager_id;
                    newRow.phone = phone;
                    newRow.address1 = address1;
                    newRow.address2 = address2;
                    newRow.city = city;
                    newRow.state = state;
                    newRow.postal_code = postal_code;
                    newRow.country = country;


                    // Sinh cau lenh de luu du lieu vao table
                    context.employees.Add(newRow);

                    // Thuc hien cau lenh de luu
                    context.SaveChanges();
                    return String.Format("Tai khoan {0} {1} da duoc tao", last_name, first_name);


                }
            }
            catch (Exception ex)
            {
                return String.Format("Co loi xay ra, thong tin loi {0} {1}", last_name, first_name);
            }

        }
    }
}