using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VATReturn.Models
{
    public class Treasury
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "ট্রেজারী চালান নং")]
        public string TreasuryNo { get; set; }

        [Display(Name = "তারিখ")]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        [Required]
        [Display(Name = "ব্যাংক")]
        public string Bank { get; set; }

        [Required]
        [Display(Name = "ব্রাঞ্চ")]
        public string Branch { get; set; }

        [Required]
        [Display(Name = "পরিমাণ")]
        public double? Quantity { get; set; }

        public InstitutionInfo InstitutionInfo { get; set; }
        public int InstitutionInfoId { get; set; }  
    }
}