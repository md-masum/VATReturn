using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VATReturn.ViewModel
{
    public class ReturnCalculationViewModel
    {
        // For number 11
        [Required]
        [Display(Name = "মোট রেয়াত যোগ্য কর")]
        public double? TotalRebateTax { get; set; }

        // For number 12
        [Required]
        [Display(Name = "অন্যান্য সমন্বয়করণ (রেয়াত/পাওনা/উৎসে কর্তন)")]
        public double? OtherCoordination { get; set; }

        // For number 13
        [Required]
        [Display(Name = "পূর্ববর্তী মাসের জের")]
        public double? PreviousMonthOdds { get; set; }

        // For number 14
        [Display(Name = "সর্বমোট রেয়াত")]
        public double? TotalRevenue { get; set; }
    }
}