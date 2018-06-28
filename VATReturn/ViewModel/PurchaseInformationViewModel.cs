using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VATReturn.Models;

namespace VATReturn.ViewModel
{
    public class PurchaseInformationViewModel
    {
        // For number 7
        [Required]
        public double? LocalLvlTaxPurchasePrise { get; set; }

        [Required]
        public double? LocalLvlTaxTaxableTax { get; set; }

        // For number 8
        public double? ImportPurchasePrise { get; set; }

        public double? ImportTaxableTax { get; set; }

        // For number 9
        public double? RebateExportPrise { get; set; }

        public double? RebateExportTax { get; set; }

        // For number 10
        public double? ExemptProductsPrise { get; set; }
    }
}