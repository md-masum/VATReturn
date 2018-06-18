using System.ComponentModel.DataAnnotations;

namespace VATReturn.Models
{
    public class RebateExport
    {
        public int Id { get; set; }

        [Display(Name = "কাস্টমস ডিউটি")]
        public double? CustomsDuty { get; set; }

        [Display(Name = "রেগুলেটরি ডিউটি")]
        public double? RegulatoryDuties { get; set; }

        [Display(Name = "সম্পূরক শুল্ক")]
        public double? SupplementaryDuty { get; set; }

        public InstitutionInfo InstitutionInfo { get; set; }
        [Display(Name = "প্রতিষ্ঠান নাম্বার")]
        public int InstitutionInfoId { get; set; }
    }
}