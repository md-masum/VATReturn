using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VATReturn.Models;

namespace VATReturn.ViewModel
{
    public class FinalAccountViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "নিট প্রদেয়")]
        public double? NetPayable { get; set; }

        [Required]
        [Display(Name = "ট্রেজারীতে জমা")]
        public double? DepositedTreasury { get; set; }

        [Display(Name = "পরবর্তী মাসের প্রারম্ভিক জের")]
        public double? NextMonth { get; set; }

        [Display(Name = "পরিদপ্তর হইতে প্রতার্পণ")]
        public double? RefugeesDirectorate { get; set; }

        [Display(Name = "উৎসে কর্তিত মোট মুসকের পরিমান")]
        public double? TotalGrocersDischarged { get; set; }
    }
}