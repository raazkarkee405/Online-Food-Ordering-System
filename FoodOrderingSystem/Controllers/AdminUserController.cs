using FoodOrderingSystem.Models;
using OnlineFoodOrderingSystem.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOrderingSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUserController : Controller
    {
        // GET: AdminUser
        OnlineFoodDBEntities _db = new OnlineFoodDBEntities();
        public ActionResult ManageUser()
        {
            return View();
        }
        public JsonResult GetData()
        {
            using (OnlineFoodDBEntities db = new OnlineFoodDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<UserViewModel> lstitem = new List<UserViewModel>();
                var lst = db.tblUsers.ToList();
                foreach (var item in lst)
                {
                    lstitem.Add(new UserViewModel() { UserId = item.UserId, Username = item.Username, Photo = item.Photo });
                }
                return Json(new { data = lstitem }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AddOrEdit()
        {
            return View();
        }
        [HttpPost]

        public ActionResult AddOrEdit(UserViewModel uv)
        {
            tblUser tb = new tblUser();
            tb.Username = uv.Username;
            tb.Password = uv.Password;

            HttpPostedFileBase fup = Request.Files["Photo"];
            if (fup != null)
            {
                if (fup.FileName != "")
                {
                    tb.Photo = fup.FileName;
                    fup.SaveAs(Server.MapPath("~/images/UserImages/" + fup.FileName));
                }
            }
            _db.tblUsers.Add(tb);
            _db.SaveChanges();

            UserRole ud = new UserRole();
            ud.UserId = tb.UserId;
            ud.UserRolesId = 1;
            _db.UserRoles.Add(ud);
            _db.SaveChanges();
            ViewBag.Message = "User Created Successfully";


            return View();
        }
        [HttpPost]

        public ActionResult Delete(int id)
        {
            using (OnlineFoodDBEntities db = new OnlineFoodDBEntities())
            {
                tblUser sm = db.tblUsers.Where(x => x.UserId == id).FirstOrDefault();
                db.tblUsers.Remove(sm);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}