using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VATReturn.Models;

namespace VATReturn.ViewModel
{
    public class InstitutionViewModel
    {
        public string TaxId { get; set; }

        public InstitutionInfo InstitutionInfo { get; set; }
    }
}