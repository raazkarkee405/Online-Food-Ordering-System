using FoodOrderingSystem.Models;
using OnlineFoodOrderingSystem.Models.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace FoodOrderingSystem.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        OnlineFoodDBEntities _db = new OnlineFoodDBEntities();
        public ActionResult Index()
        {
            return View(_db.tblBanners.ToList());
        }
        public ActionResult Item()
        {
            List<tblItem> myitem = ItemDB.GetallRecentItem();
            return PartialView("_Item", myitem);
        }
        
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(UserViewModel uv)
        {
            tblUser tbl = _db.tblUsers.Where(u => u.Username == uv.Username).FirstOrDefault();
            if (tbl != null)
            {
                return Json(new { success = false, message = "User Already Register" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                tblUser tb = new tblUser();
                tb.Username = uv.Username;
                tb.Password = uv.Password;
                _db.tblUsers.Add(tb);
                _db.SaveChanges();

                UserRole ud = new UserRole();
                ud.UserId = tb.UserId;
                ud.UserRolesId = 2;
                _db.UserRoles.Add(ud);
                _db.SaveChanges();
                return Json(new { success = true, message = "User Register Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
       
        public ActionResult ForgetPassword()
        {
            return View();

            //return RedirectToAction("Index", "Home");
        }
        [ValidateOnlyIncomingValuesAttribute]
        [HttpPost]

        public ActionResult ForgetPassword(UserViewModel uv)
        {

            if (ModelState.IsValid)
            {
                //https://www.google.com/settings/security/lesssecureapps
                //Make Access for less secure apps=true

                string from = "gfccafe123@gmail.com";
                using (MailMessage mail = new MailMessage(from, uv.Username))
                {
                    try
                    {
                        tblUser tb = _db.tblUsers.Where(u => u.Username == uv.Username).FirstOrDefault();
                        if (tb != null)
                        {
                            mail.Subject = "Password Recovery";
                            mail.Body = "Your Password is:" + tb.Password;

                            mail.IsBodyHtml = false;
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "smtp.gmail.com";
                            smtp.EnableSsl = true;
                            NetworkCredential networkCredential = new NetworkCredential(from, "suneel!@#123");
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = networkCredential;
                            smtp.Port = 587;
                            smtp.Send(mail);
                            ViewBag.Message = "Your Password Is Sent to your email";
                        }
                        else
                        {
                            ViewBag.Message = "email Doesnot Exist in Database";
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {

                    }

                }

            }
            return View();


            //return RedirectToAction("Index", "Home");
        }
        public ActionResult ShoppingCartList()
        {
            return View();
        }
        public ActionResult ProductList(string search, int? page, int id = 0)
        {

            if (id != 0)
            {

                return View(_db.tblItems.Where(p => p.SubMenuId == id).ToList().ToPagedList(page ?? 1, 12));
            }
            else
            {
                if (search != "")
                {
                    return View(_db.tblItems.Where(x => x.Description.Contains(search) || x.Title.Contains(search) || search == null).ToList().ToPagedList(page ?? 1, 12));
                }
                else
                {
                    return View(_db.tblItems.ToList().ToPagedList(page ?? 1, 12));
                }

            }

        }




        //OnlineFoodDBEntities db = new OnlineFoodDBEntities();
        //public ActionResult ViewItem(int id)
        //{
        //    return PartialView("_ViewItem", db.tblItems.Find(id));
        //}
        //[HttpGet]
        //public ActionResult LoginForm()
        //{
        //    return PartialView("_LoginForm");
        //}
        //[HttpPost]
        //public ActionResult LoginForm(LoginViewModel lg)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return RedirectToAction("Dashboard", "Admin");
        //    }
        //    return PartialView("_LoginForm");
        //}


    }
}