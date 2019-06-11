using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingSystem.Models.ViewModel
{
    public class OrderDetailsViewModel
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public string ItemName { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}