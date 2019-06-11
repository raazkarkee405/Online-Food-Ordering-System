using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingSystem.Models.ViewModel
{
    public class ItemViewModel
    {
        public int ItemId { get; set; }
        [Required(ErrorMessage ="Category Required")]
        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        [Required(ErrorMessage = "Title Required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Price Required")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Description Required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Small Image Required")]
        public string SmallImage { get; set; }
        [Required(ErrorMessage = "Large Image Required")]
        public string LargeImage { get; set; }
        public string IsAvailable { get; set; }
    }
}