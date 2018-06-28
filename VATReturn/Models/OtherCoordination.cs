using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VATReturn.Models
{
    public class OtherCoordination
    {
        public int Id { get; set; }

        [Display(Name = "অন্যান্য রেয়াত")]
        public double? OtherRebates { get; set; }

        [Display(Name = "পাওনা")]
        public double? Owing { get; set; }

        [Display(Name = "উৎসে কর্তন")]
        public double? SourceCut { get; set; }

        [Display(Name = "নাই")]
        public bool Blank { get; set; }

        [Required]
        [Display(Name = "তারিখ")]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        public InstitutionInfo InstitutionInfo { get; set; }
        public int InstitutionInfoId { get; set; }
    }
}