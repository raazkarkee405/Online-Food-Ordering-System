using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderingSystem.Models
{
    public static class ItemDB
    {
        public static List<tblItem> GetallRecentItem()
        {
            using (var context = new OnlineFoodDBEntities())
            {

                return context.tblItems.Where(u => u.IsAvailable == "Special").Take(8).ToList();
            }
        }
        public static List<tblItem> GetAllFoodRecentItem()
        {
            using (var context = new OnlineFoodDBEntities())
            {
                return context.tblItems.Take(8).ToList();
            }
        }
        public static List<tblItem> GetAllFood()
        {
            using (var context = new OnlineFoodDBEntities())
            {
                return context.tblItems.ToList();
            }
        }
    }
}