using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace VATReturn.Models
{
    public class ValueAddedTax
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "কর মেয়াদ")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Display(Name = "শূন্য দাখিলপত্র (কর মেয়াদে কোন ক্রয় বিক্রয় হয় নাই)")]
        public bool ZeroReturns { get; set; }

        // For Number 1
        [Required]
        public int? Percentage { get; set; }
        [Required]
        public double? TaxableGoodsSalePrice { get; set; }

        public double? TaxableGoodsSupplementaryDuty { get; set; }

        public double? TaxableGoodsValueAddedTax { get; set; }

        // For Number 2
        public double? ZeroRatedSalePrice { get; set; }

        public double? ZeroRatedSupplementaryDuty { get; set; }

        public double? ZeroRatedValueAddedTax { get; set; }

        // For Number 3
        public double? ExemptSalePrice { get; set; }

        public double? ExemptSupplementaryDuty { get; set; }

        public double? ExemptValueAddedTax { get; set; }

        // For Number 4
        public double? TotalTaxPayable { get; set; }

        // This section for Other Consolidation
        [Display(Name = "উৎসে কর্তন")]
        public double? SourceCut { get; set; }
        [Display(Name = "বকেয়া")]
        public double? Owing { get; set; }
        [Display(Name = "জরিমানা")]
        public double? Amercement { get; set; }
        [Display(Name = "অর্থদণ্ড")]
        public double? Fine { get; set; }
        [Display(Name = "স্থান বা স্থাপনা ভাড়া")]
        public double? RentSppace { get; set; }
        // Section End

        // For Number 5
        public double? OtherConsolidation { get; set; }

        // For Number 6
        public double? TotalPayable { get; set; }

        // For number 7
        public double? LocalLvlTaxPurchasePrise { get; set; }

        public double? LocalLvlTaxTaxableTax { get; set; }

        // For number 8
        public double? ImportPurchasePrise { get; set; }

        public double? ImportTaxableTax { get; set; }

        // For number 9
        public double? RebateExportPrise { get; set; }

        public double? RebateExportTax { get; set; }

        // For number 10
        public double? ExemptProductsPrise { get; set; }

        // For number 11
        [Display(Name = "মোট রেয়াত যোগ্য কর")]
        public double? TotalRebateTax { get; set; }

        // For number 12
        [Display(Name = "অন্যান্য সমন্বয়করণ (রেয়াত/পাওনা/উৎসে কর্তন)")]
        public double? OtherCoordination { get; set; }

        // For number 13
        [Display(Name = "পূর্ববর্তী মাসের জের")]
        public double? PreviousMonthOdds { get; set; }

        // For number 14
        [Display(Name = "সর্বমোট রেয়াত")]
        public double? TotalRevenue { get; set; }

        // For number 15
        [Display(Name = "নিট প্রদেয়")]
        public double? NetPayable { get; set; }

        // For number 16
        [Display(Name = "ট্রেজারীতে জমা")]
        public double? DepositedTreasury { get; set; }

        // For number 17
        [Display(Name = "পরবর্তী মাসের প্রারম্ভিক জের")]
        public double? NextMonth { get; set; }

        // For number 18
        [Display(Name = "পরিদপ্তর হইতে প্রতার্পণ")]
        public double? RefugeesDirectorate { get; set; }

        // For number 19
        [Display(Name = "উৎসে কর্তিত মোট মুসকের পরিমান")]
        public double? TotalGrocersDischarged { get; set; }

        [Required]
        public string Bank { get; set; }
        [Required]
        public string Branch { get; set; }

        [Display(Name = "তারিখ")]
        [DataType(DataType.Date)]
        public DateTime? DateTime { get; set; }

        public InstitutionInfo InstitutionInfo { get; set; }
        public int InstitutionInfoId { get; set; }  
    }
}