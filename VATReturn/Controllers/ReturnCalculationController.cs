using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using VATReturn.Models;
using VATReturn.ViewModel;

namespace VATReturn.Controllers
{
    public class ReturnCalculationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReturnCalculationController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: ReturnCalculation
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

                double forNoEleven = 0;
                double forNoTwelve = 0;


                var otherCoordination = _context.OtherCoordinations.Where(c => c.InstitutionInfoId == id && (c.DateTime <= endDate && c.DateTime >= startDate));

                forNoEleven = valueAddedTax.LocalLvlTaxTaxableTax.GetValueOrDefault() +
                              valueAddedTax.ImportTaxableTax.GetValueOrDefault() +
                              valueAddedTax.RebateExportTax.GetValueOrDefault();
                foreach (var data in otherCoordination)
                {
                    forNoTwelve = forNoTwelve + data.OtherRebates.GetValueOrDefault() + 
                                  data.Owing.GetValueOrDefault() +
                                  data.SourceCut.GetValueOrDefault();

                }

                var returnCalculation = new ReturnCalculationViewModel
                {
                    TotalRebateTax = forNoEleven,
                    OtherCoordination = forNoTwelve
                };

                return View(returnCalculation);
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ReturnCalculationViewModel returnCalculationViewModel)
        {
            if (!ModelState.IsValid)
            {
                if (Session["ValueAddedTax"] == null)
                    return HttpNotFound();
                var data = Session["ValueAddedTax"] as ValueAddedTax;
                return RedirectToAction("Index", new {id = data.InstitutionInfoId});
            }

            double noEleven = 0;
            double noTwelve = 0;
            double noThirteen = 0;
            double noForteen = 0;

            noEleven = returnCalculationViewModel.TotalRebateTax.GetValueOrDefault();
            noTwelve = returnCalculationViewModel.OtherCoordination.GetValueOrDefault();
            noThirteen = returnCalculationViewModel.PreviousMonthOdds.GetValueOrDefault();
            noForteen = noEleven + noTwelve + noThirteen;

            var sessionData = Session["ValueAddedTax"] as ValueAddedTax;
            if (sessionData != null)
            {
                sessionData.TotalRebateTax = noEleven;
                sessionData.OtherCoordination = noTwelve;
                sessionData.PreviousMonthOdds = noThirteen;
                sessionData.TotalRevenue = noForteen;
            }

            if (sessionData != null)
                return RedirectToAction("Index", "Treasuries", new {id = sessionData.InstitutionInfoId});
            return HttpNotFound();
        }
    }
}