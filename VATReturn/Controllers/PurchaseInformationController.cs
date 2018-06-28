using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VATReturn.Models;
using VATReturn.ViewModel;

namespace VATReturn.Controllers
{
    public class PurchaseInformationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseInformationController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: PurchaseInformation
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id != 0)
            {
                var valueAddedTax = Session["ValueAddedTax"] as ValueAddedTax;
                if (valueAddedTax == null)
                {
                    return HttpNotFound();
                }

                DateTime now = valueAddedTax.Date.GetValueOrDefault();
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                var localLvlTax = _context.LocalLvlTaxs.Where(c => c.InstitutionInfoId == id && (c.DateTime <=endDate && c.DateTime >= startDate));
                if (!localLvlTax.Any())
                {
                    return HttpNotFound();
                }

                var importTaxableGoods = _context.ImportTaxableGoodses.Where(c => c.InstitutionInfoId == id && (c.DateTime <= endDate && c.DateTime >= startDate));
                var rebateExports = _context.RebateExports.Where(c => c.InstitutionInfoId == id && (c.DateTime <= endDate && c.DateTime >= startDate));

                double forNoSavnePurchase = 0;
                double forNoSavneVat = 0;
                double forNoEightPurchase = 0;
                double forNoEightVat = 0;
                double forNoNinePurchase = 0;
                double forNoNineVat = 0;

                foreach (var data in localLvlTax)
                {
                    forNoSavnePurchase = forNoSavnePurchase + data.Price.GetValueOrDefault();
                    forNoSavneVat = forNoSavneVat + data.Vat.GetValueOrDefault();
                }

                foreach (var data in importTaxableGoods)
                {
                    forNoEightPurchase = forNoEightPurchase + data.Price.GetValueOrDefault();
                    forNoEightVat = forNoEightVat + data.Vat.GetValueOrDefault();
                }

                foreach (var data in rebateExports)
                {
                    forNoNinePurchase = forNoNinePurchase + data.CustomsDuty.GetValueOrDefault();
                    forNoNineVat = forNoNineVat + data.SupplementaryDuty.GetValueOrDefault();
                }

                var purchaseInfo = new PurchaseInformationViewModel
                {
                    LocalLvlTaxPurchasePrise = forNoSavnePurchase,
                    LocalLvlTaxTaxableTax = forNoSavneVat,

                    ImportPurchasePrise = forNoEightPurchase,
                    ImportTaxableTax = forNoEightVat,

                    RebateExportPrise = forNoNinePurchase,
                    RebateExportTax = forNoNineVat,
                };

                return View(purchaseInfo);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult Save(PurchaseInformationViewModel purchaseInformationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Session["ValueAddedTax"] is ValueAddedTax valueAddedTax)
            {
                valueAddedTax.LocalLvlTaxPurchasePrise = purchaseInformationViewModel.LocalLvlTaxPurchasePrise;
                valueAddedTax.LocalLvlTaxTaxableTax = purchaseInformationViewModel.LocalLvlTaxTaxableTax;

                valueAddedTax.ImportPurchasePrise = purchaseInformationViewModel.ImportPurchasePrise;
                valueAddedTax.ImportTaxableTax = purchaseInformationViewModel.ImportTaxableTax;

                valueAddedTax.RebateExportPrise = purchaseInformationViewModel.RebateExportPrise;
                valueAddedTax.RebateExportTax = purchaseInformationViewModel.RebateExportTax;

                valueAddedTax.ExemptProductsPrise = purchaseInformationViewModel.ExemptProductsPrise;


                return RedirectToAction("Index", "OtherCoordinations", new {id = valueAddedTax.InstitutionInfoId});

            }

            return HttpNotFound();
        }
    }
}