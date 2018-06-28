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
    public class FinalAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FinalAccountController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: FinalAccount
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == 0)
                return HttpNotFound();

            var data = Session["ValueAddedTax"] as ValueAddedTax;

            if (data != null)
            {
                var finalAccount = new FinalAccountViewModel();

                DateTime now = data.Date.GetValueOrDefault();
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                var treasury = _context.Treasuries.Where(c =>
                    c.InstitutionInfoId == id && (c.DateTime <= endDate && c.DateTime >= startDate));

                // no 15
                finalAccount.NetPayable = data.TotalPayable - data.TotalRevenue;

                // no 16
                double noSixteen = 0;
                foreach (var value in treasury)
                {
                    noSixteen = noSixteen + value.Quantity.GetValueOrDefault();
                }

                finalAccount.DepositedTreasury = noSixteen;

                // no 17
                finalAccount.NextMonth = finalAccount.DepositedTreasury.GetValueOrDefault() - finalAccount.NetPayable.GetValueOrDefault();

                finalAccount.Id = (int)id;

                return View(finalAccount);

            }

            return RedirectToAction("Index", "Instution");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(FinalAccountViewModel finalAccountViewModel)
        {
            if (!ModelState.IsValid)
            {
                if (finalAccountViewModel.Id != 0)
                    return RedirectToAction("Index", "FinalAccount", new { id = finalAccountViewModel.Id });
                return RedirectToAction("Index", "Instution");
            }

            var data = Session["ValueAddedTax"] as ValueAddedTax;

            if (data == null)
                return HttpNotFound();

            data.NetPayable = finalAccountViewModel.NetPayable;
            data.DepositedTreasury = finalAccountViewModel.DepositedTreasury;
            data.NextMonth = finalAccountViewModel.NextMonth;
            data.RefugeesDirectorate = finalAccountViewModel.RefugeesDirectorate;
            data.TotalGrocersDischarged = finalAccountViewModel.TotalGrocersDischarged;


            _context.ValueAddedTaxs.Add(data);
            _context.SaveChanges();

            Session.Remove("ValueAddedTax");

            return RedirectToAction("Index", "Home");
        }
    }
}