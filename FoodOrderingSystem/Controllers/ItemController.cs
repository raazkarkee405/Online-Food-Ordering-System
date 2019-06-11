using FoodOrderingSystem.Models;
using OnlineFoodOrderingSystem.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOrderingSystem.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        [Authorize(Roles = "Admin")]
        public ActionResult ManageItem()
        {
            return View();
        }
        public JsonResult GetData()
        {
            using (OnlineFoodDBEntities db = new OnlineFoodDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<ItemViewModel> lstitem = new List<ItemViewModel>();
                var lst = db.tblItems.ToList();
                foreach (var item in lst)
                {
                    lstitem.Add(new ItemViewModel() { ItemId = item.ItemId, Title = item.Title, Price = item.Price, Description = item.Description, SmallImage = item.SmallImage, LargeImage = item.LargeImage, IsAvailable = item.IsAvailable });
                }
                return Json(new { data = lstitem }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                using (OnlineFoodDBEntities db = new OnlineFoodDBEntities())
                {
                    ViewBag.SubMenus = db.tblSubMenus.ToList();
                    ViewBag.Action = "Create New Item";
                    return View(new ItemViewModel());
                }
            }
            else
            {
                using (OnlineFoodDBEntities db = new OnlineFoodDBEntities())
                {
                    ViewBag.Action = "Edit Item";
                    ViewBag.Menus = db.tblSubMenus.ToList();
                    tblItem item = db.tblItems.Where(i => i.ItemId == id).FirstOrDefault();
                    ItemViewModel itemvm = new ItemViewModel();
                    itemvm.ItemId = item.ItemId;
                    itemvm.SubMenuId = Convert.ToInt32(item.SubMenuId);
                    itemvm.Title = item.Title;
                    itemvm.Price = item.Price;
                    itemvm.Description = item.Description;
                    itemvm.SmallImage = item.SmallImage;
                    itemvm.LargeImage = item.LargeImage;
                    //HttpPostedFileBase fup = Request.Files["SmallImage"];
                    //if(fup!=null)
                    //{
                    //    if(fup.FileName!="")
                    //    {
                    //        fup.SaveAs(Server.MapPath("~/img/dogaccessories" + fup.FileName));

                    //    }
                    //}

                    //HttpPostedFileBase fup1 = Request.Files["LargeImage"];
                    //if (fup1 != null)
                    //{
                    //    if (fup1.FileName != "")
                    //    {
                    //        fup1.SaveAs(Server.MapPath("~/img/dogaccessories" + fup1.FileName));
                    //        itemvm.LargeImage = item.LargeImage;
                    //    }
                    //}



                    itemvm.IsAvailable = "Available";

                    ViewBag.SubMenus = db.tblSubMenus.ToList();

                    return View(itemvm);
                }
            }
        }

        [HttpPost]

        public ActionResult AddOrEdit(ItemViewModel ivm)
        {
            using (OnlineFoodDBEntities db = new OnlineFoodDBEntities())
            {
                if (ivm.ItemId == 0)
                {
                    tblItem itm = new tblItem();

                    itm.SubMenuId = Convert.ToInt32(ivm.SubMenuId);
                    itm.Title = ivm.Title;
                    itm.Price = ivm.Price;
                    itm.Description = ivm.Description;
                    HttpPostedFileBase fup = Request.Files["SmallImage"];
                    if (fup != null)
                    {
                        if (fup.FileName != "")
                        {
                            fup.SaveAs(Server.MapPath("~/images/ItemImages/" + fup.FileName));
                            itm.SmallImage = fup.FileName;
                        }
                    }

                    HttpPostedFileBase fup1 = Request.Files["LargeImage"];
                    if (fup1 != null)
                    {
                        if (fup1.FileName != "")
                        {
                            fup1.SaveAs(Server.MapPath("~/images/ItemImages/" + fup1.FileName));
                            itm.LargeImage = fup1.FileName;
                        }
                    }
                    itm.IsAvailable = "Available";
                    db.tblItems.Add(itm);
                    db.SaveChanges();
                    ViewBag.Message = "Updated Successfully";
                }
                else
                {
                    tblItem itm = db.tblItems.Where(i => i.ItemId == ivm.ItemId).FirstOrDefault();
                    itm.SubMenuId = Convert.ToInt32(ivm.SubMenuId);
                    itm.Title = ivm.Title;
                    itm.Price = ivm.Price;
                    itm.Description = ivm.Description;
                    HttpPostedFileBase fup = Request.Files["SmallImage"];
                    if (fup != null)
                    {
                        if (fup.FileName != "")
                        {
                            fup.SaveAs(Server.MapPath("~/images/ItemImages/" + fup.FileName));
                            itm.SmallImage = fup.FileName;
                        }
                    }

                    HttpPostedFileBase fup1 = Request.Files["LargeImage"];
                    if (fup1 != null)
                    {
                        if (fup1.FileName != "")
                        {
                            fup1.SaveAs(Server.MapPath("~/images/ItemImages/" + fup1.FileName));
                            itm.LargeImage = fup1.FileName;
                        }
                    }


                    db.SaveChanges();
                    ViewBag.Message = "Updated Successfully";

                }
                ViewBag.SubMenus = db.tblSubMenus.ToList();
                return View(new ItemViewModel());

            }


        }

        [HttpPost]

        public ActionResult Delete(int id)
        {
            using (OnlineFoodDBEntities db = new OnlineFoodDBEntities())
            {
                tblItem sm = db.tblItems.Where(x => x.ItemId == id).FirstOrDefault();
                db.tblItems.Remove(sm);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}