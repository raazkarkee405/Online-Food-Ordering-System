using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingSystem.Models.ViewModel
{
    public class SubMenuViewModel
    {
        public int SubMenuId { get; set; }
        [Required]
        public string SubMenuName { get; set; }
    }
}