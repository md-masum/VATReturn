using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VATReturn.Models
{
    public class ImportTaxableGoods
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "B/E No")]
        public string BandENo { get; set; }

        [Required]
        [Display(Name = "তারিখ")]
        [DataType(DataType.Date)]
        public DateTime? DateTime { get; set; }

        [Required]
        [Display(Name = "মূল্য")]
        public double? Price { get; set; }

        [Required]
        [Display(Name = "ভ্যাট")]
        public double? Vat { get; set; }

        public InstitutionInfo InstitutionInfo { get; set; }
        [Display(Name = "প্রতিষ্ঠান নাম্বার")]
        public int InstitutionInfoId { get; set; }
    }
}