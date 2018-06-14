using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VATReturn.Models;
using VATReturn.ViewModel;

namespace VATReturn.Controllers
{
    public class InstutionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstutionController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Instution
        public ActionResult Index()
        {
            var instution = new InstitutionInfo();
            return View(instution);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert(InstitutionInfo institution)
        {
            if (string.IsNullOrWhiteSpace(institution.TaxId))
            {
                return RedirectToAction("Index", "Instution");
            }

            var data = _context.InstitutionInfos.SingleOrDefault(c => c.TaxId == institution.TaxId);

            if (data == null)
            {
                var info = new InstitutionViewModel
                {
                    TaxId = institution.TaxId,
                    InstitutionInfo = new InstitutionInfo
                    {
                        TaxId = institution.TaxId
                    }
                    
                };
                
                return View(info);
            }

            var instuteInfo = new InstitutionViewModel
            {
                InstitutionInfo = institution
            };


            return View(instuteInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(InstitutionInfo institution)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Instution");
            }

            if (institution.Id == 0)
            {
                _context.InstitutionInfos.Add(institution);
                _context.SaveChanges();

                int institutionId = institution.Id;
                return RedirectToAction("Index", "ValueAddedTax", new {id = institutionId});
            }
            else
            {
                var data = _context.InstitutionInfos.SingleOrDefault(c => c.Id == institution.Id);

                if (data != null)
                {
                    int institutionId = data.Id;
                    return RedirectToAction("Index", "ValueAddedTax", new {id = institutionId});
                }
            }

            return RedirectToAction("Index", "Instution");
        }
    }
}