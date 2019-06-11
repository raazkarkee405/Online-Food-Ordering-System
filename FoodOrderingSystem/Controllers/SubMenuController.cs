using FoodOrderingSystem.Models;
using OnlineFoodOrderingSystem.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOrderingSystem.Controllers
{
    public class SubMenuController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult ManageSubMenu()
        {
            return View();
        }
        public JsonResult GetData()
        {
            using (OnlineFoodDBEntities db = new OnlineFoodDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<SubMenuViewModel> lst = new List<SubMenuViewModel>();
                var empList = db.tblSubMenus.Select(x => new { SubMenuId = x.SubMenuId, SubMenuName = x.SubMenuName }).ToList();
                foreach (var item in empList)
                {
                    lst.Add(new SubMenuViewModel() { SubMenuId = item.SubMenuId, SubMenuName = item.SubMenuName });
                }
                return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                using (OnlineFoodDBEntities db = new OnlineFoodDBEntities())
                {

                    return View(new SubMenuViewModel());
                }
            }
            else
            {
                using (OnlineFoodDBEntities db = new OnlineFoodDBEntities())
                {
                    SubMenuViewModel sub = new SubMenuViewModel();
                    var menu = db.tblSubMenus.Where(x => x.SubMenuId == id).FirstOrDefault();
                    sub.SubMenuId = menu.SubMenuId;
                    sub.SubMenuName = menu.SubMenuName;
                    return View(sub);
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(SubMenuViewModel sm)
        {
            using (OnlineFoodDBEntities db = new OnlineFoodDBEntities())
            {
                if (sm.SubMenuId == 0)
                {
                    tblSubMenu tb = new tblSubMenu();
                    tb.SubMenuName = sm.SubMenuName;
                    db.tblSubMenus.Add(tb);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    tblSubMenu tbm = db.tblSubMenus.Where(m => m.SubMenuId == sm.SubMenuId).FirstOrDefault();
                    tbm.SubMenuName = sm.SubMenuName;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (OnlineFoodDBEntities db = new OnlineFoodDBEntities())
            {
                tblSubMenu sm = db.tblSubMenus.Where(x => x.SubMenuId == id).FirstOrDefault();
                db.tblSubMenus.Remove(sm);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}