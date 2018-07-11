using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VATReturn.Models;
using VATReturn.ViewModel;
using System.Data.Entity;

namespace VATReturn.Controllers
{
    [Authorize]
    public class VatReturnFormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VatReturnFormController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customer
        public ActionResult Index()
        {
            var customer = new InstitutionInfo();
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowData(InstitutionInfo institutionInfo)
        {
            if (string.IsNullOrWhiteSpace(institutionInfo.TaxId))
            {
                return RedirectToAction("Index", "VatReturnForm");
            }
            else
            {
                var data = _context.InstitutionInfos.SingleOrDefault(c => c.TaxId == institutionInfo.TaxId);

                if (data == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    var taxData = _context.ValueAddedTaxs.Where(c => c.InstitutionInfoId == data.Id).ToList();

                    var viewData = new ShowDataViewModel
                    {
                        InstitutionInfo = data,
                        ValueAddedTaxs = taxData
                    };

                    return View(viewData);
                }
            }

        }

        public ActionResult Show(int id)
        {
            var taxData = _context.ValueAddedTaxs.Include(c => c.InstitutionInfo).SingleOrDefault(c => c.Id == id);
            if (taxData != null)
            {
                return View(taxData);
            }
            return HttpNotFound();
        }
    }
}