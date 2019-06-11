using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingSystem.Models.ViewModel
{
    public class EventViewModel
    {
        public int EventID { get; set; }
        [Required(ErrorMessage ="Subject Required")]
        public string Subject { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Start Date Required")]

        public string Start { get; set; }
      
        public string End { get; set; }
        [Required(ErrorMessage = "Theme Color Required")]
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }
    }
}