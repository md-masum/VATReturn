using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VATReturn.Models;

namespace VATReturn.ViewModel
{
    public class ShowDataViewModel
    {
        public InstitutionInfo InstitutionInfo { get; set; }
        public IEnumerable<ValueAddedTax> ValueAddedTaxs { get; set; }

        [Required]
        [Display(Name = "কর মেয়াদ")]
        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}")]
        public System.DateTime? Date { get; set; }
    }
}