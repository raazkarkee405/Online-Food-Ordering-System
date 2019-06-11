using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderingSystem.Models
{
    public class MyMenu
    {
        public static List<tblSubMenu> GetMenus()
        {
            using (var context = new OnlineFoodDBEntities())
            {
                return context.tblSubMenus.ToList();
            }
        }
        public static List<tblSubMenu> GetSubMenus(int menuid)
        {
            using (var context = new OnlineFoodDBEntities())
            {
                return context.tblSubMenus.Where(u => u.SubMenuId == menuid).ToList();
            }
        }
    }
}