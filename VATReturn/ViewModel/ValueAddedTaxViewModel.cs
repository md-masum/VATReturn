using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VATReturn.Models;

namespace VATReturn.ViewModel
{
    public class ValueAddedTaxViewModel
    {
        public InstitutionInfo InstitutionInfo { get; set; }
        public ValueAddedTax ValueAddedTax { get; set; }
    }
}