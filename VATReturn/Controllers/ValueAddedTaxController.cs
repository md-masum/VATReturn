using System;
using System.Linq;
using System.Web.Mvc;
using VATReturn.Models;
using VATReturn.ViewModel;

namespace VATReturn.Controllers
{
    public class ValueAddedTaxController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ValueAddedTaxController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: ValueAddedTax
        public ActionResult Index(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Instution");
            }

            var valueAddedTax = new ValueAddedTaxViewModel
            {
                InstitutionInfo = _context.InstitutionInfos.SingleOrDefault(c => c.Id == id),
                ValueAddedTax = new ValueAddedTax
                {
                    InstitutionInfoId = id
                }
            };

            return View(valueAddedTax);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTaxInfo(ValueAddedTax valueAddedTax)
        {
            if (!ModelState.IsValid)
            {
                if (valueAddedTax.InstitutionInfoId != 0)
                    return RedirectToAction("Index", "ValueAddedTax", new { id = valueAddedTax.InstitutionInfoId });

                return RedirectToAction("Index", "Instution");
            }


            // Assagin Data
            double taxableGoodsSalePrice = valueAddedTax.TaxableGoodsSalePrice.GetValueOrDefault();
            double taxableGoodsSupplementaryDuty = valueAddedTax.TaxableGoodsSupplementaryDuty.GetValueOrDefault();
            double taxableGoodsValueAddedTax = valueAddedTax.TaxableGoodsValueAddedTax.GetValueOrDefault();
            double zeroRatedSalePrice = valueAddedTax.ZeroRatedSalePrice.GetValueOrDefault();
            double zeroRatedSupplementaryDuty = valueAddedTax.ZeroRatedSupplementaryDuty.GetValueOrDefault();
            double zeroRatedValueAddedTax = valueAddedTax.ZeroRatedValueAddedTax.GetValueOrDefault();
            double exemptSalePrice = valueAddedTax.ExemptSalePrice.GetValueOrDefault();
            double exemptSupplementaryDuty = valueAddedTax.ExemptSupplementaryDuty.GetValueOrDefault();
            double exemptValueAddedTax = valueAddedTax.ExemptValueAddedTax.GetValueOrDefault();
            double otherConsolidation = valueAddedTax.OtherConsolidation.GetValueOrDefault();
            double totalTaxPayable = valueAddedTax.TotalTaxPayable.GetValueOrDefault();
            double totalPayable = valueAddedTax.TotalPayable.GetValueOrDefault();

            double sourceCut = valueAddedTax.SourceCut.GetValueOrDefault();
            double owing = valueAddedTax.Owing.GetValueOrDefault();
            double amercement = valueAddedTax.Amercement.GetValueOrDefault();
            double fine = valueAddedTax.Fine.GetValueOrDefault();
            double rentSppace = valueAddedTax.RentSppace.GetValueOrDefault();


            if (valueAddedTax.Percentage != null && valueAddedTax.Percentage == 15)
            {
                //For number 1
                taxableGoodsValueAddedTax =
                    (taxableGoodsSalePrice + taxableGoodsSupplementaryDuty) * 15 / 100;

                //For number 4
                totalTaxPayable = taxableGoodsSupplementaryDuty + zeroRatedSupplementaryDuty +
                                  exemptSupplementaryDuty + taxableGoodsValueAddedTax + zeroRatedValueAddedTax +
                                  exemptValueAddedTax;

                //For number 5
                otherConsolidation = sourceCut + owing + amercement + fine + rentSppace;

                //For number 6
                totalPayable = totalTaxPayable + otherConsolidation;
            }
            else if (valueAddedTax.Percentage != null && valueAddedTax.Percentage == 4)
            {
                //For number 1
                taxableGoodsValueAddedTax =
                    (taxableGoodsSalePrice + taxableGoodsSupplementaryDuty) * 4 / 100;

                //For number 4
                totalTaxPayable = taxableGoodsSupplementaryDuty + zeroRatedSupplementaryDuty +
                                  exemptSupplementaryDuty + taxableGoodsValueAddedTax + zeroRatedValueAddedTax +
                                  exemptValueAddedTax;

                //For number 5
                otherConsolidation = sourceCut + owing + amercement + fine + rentSppace;

                //For number 6
                totalPayable = totalTaxPayable + otherConsolidation;
            }


            var data = new ValueAddedTax
            {
                Id = valueAddedTax.Id,
                Date = valueAddedTax.Date,
                ZeroReturns = false,
                Percentage = valueAddedTax.Percentage,
                TaxableGoodsSalePrice = taxableGoodsSalePrice,
                TaxableGoodsSupplementaryDuty = taxableGoodsSupplementaryDuty,
                TaxableGoodsValueAddedTax = taxableGoodsValueAddedTax,
                ZeroRatedSalePrice = zeroRatedSalePrice,
                ZeroRatedSupplementaryDuty = zeroRatedSupplementaryDuty,
                ZeroRatedValueAddedTax = zeroRatedValueAddedTax,
                ExemptSalePrice = exemptSalePrice,
                ExemptSupplementaryDuty = exemptSupplementaryDuty,
                ExemptValueAddedTax = exemptValueAddedTax,
                TotalTaxPayable = totalTaxPayable,
                SourceCut = sourceCut,
                Owing = owing,
                Amercement = amercement,
                Fine = fine,
                RentSppace = rentSppace,
                OtherConsolidation = otherConsolidation,
                TotalPayable = totalPayable,
                DateTime = valueAddedTax.DateTime,
                InstitutionInfoId = valueAddedTax.InstitutionInfoId,
                Branch = valueAddedTax.Branch,
                Bank = valueAddedTax.Bank
            };

            Session.Remove("ValueAddedTax");
            Session["ValueAddedTax"] = data;

            return RedirectToAction("Index", "ValueAddedTax", new { id = valueAddedTax.InstitutionInfoId });
        }

        public ActionResult ZeroReturn(int id)
        {
            if (id != 0)
            {
                var data = new ValueAddedTax
                {
                    Date = DateTime.Today,
                    ZeroReturns = true,
                    Percentage = 0,
                    TaxableGoodsSalePrice = 0,
                    Bank = "null",
                    Branch = "null",
                    DateTime = DateTime.Today,
                    InstitutionInfoId = id
                };

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                var confirmation =
                    _context.ValueAddedTaxs.Where(c => c.Date <= endDate && c.Date >= startDate);

                if (confirmation.Any())
                    return RedirectToAction("Error");

                _context.ValueAddedTaxs.Add(data);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Instution");
        }

        public ActionResult Error()
        {
            ViewBag.massage = "Data Already Exixt....";
            return View();
        }
    }
}