using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VATReturn.Models
{
    public class LocalLvlTax
    {
        public int Id { get; set; }

        [Display(Name = "চালান নাম্বার")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "বিক্রেতার VAT Reg No")]
        public string VatRegNo { get; set; }

        [Display(Name = "নাই")]
        public bool Blink { get; set; }

        [Required]
        [Display(Name = "তারিখ")]
        [DataType(DataType.Date)]
        public DateTime? DateTime { get; set; }

        [Required]
        [Display(Name = "মূল্য")]
        public double? Price { get; set; }

        [Required]
        [Display(Name = "VAT")]
        public double? Vat { get; set; }

        public InstitutionInfo InstitutionInfo { get; set; }
        public int InstitutionInfoId { get; set; }  
    }
}