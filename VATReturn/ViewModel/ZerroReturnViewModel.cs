using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VATReturn.ViewModel
{
    public class ZerroReturnViewModel
    {
        [Required]
        public int InstuteId { get; set; }

        [Required]
        [Display(Name = "কর মেয়াদ")]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
    }
}