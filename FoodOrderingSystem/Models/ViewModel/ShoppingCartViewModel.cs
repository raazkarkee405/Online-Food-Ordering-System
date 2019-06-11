using FoodOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingSystem.Models.ViewModel
{
    public class ShoppingCartViewModel
    {
      public List<tblCart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}