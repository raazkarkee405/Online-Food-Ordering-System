using FoodOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOrderingSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        OnlineFoodDBEntities _db = new OnlineFoodDBEntities();
        [Authorize(Roles = "Admin")]
        public ActionResult DashBoard()
        {
            return View();
        }

    }
}