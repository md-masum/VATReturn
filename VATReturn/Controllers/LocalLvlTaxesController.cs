using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VATReturn.Models;

namespace VATReturn.Controllers
{
    public class LocalLvlTaxesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LocalLvlTaxes
        public async Task<ActionResult> Index(int id)
        {
            var localLvlTaxs = db.LocalLvlTaxs.Include(l => l.InstitutionInfo).Where(c => c.InstitutionInfoId == id);
            return View(await localLvlTaxs.ToListAsync());
        }

        // GET: LocalLvlTaxes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocalLvlTax localLvlTax = await db.LocalLvlTaxs.FindAsync(id);
            if (localLvlTax == null)
            {
                return HttpNotFound();
            }
            return View(localLvlTax);
        }

        // GET: LocalLvlTaxes/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == 0)
                return HttpNotFound();

            var data = new LocalLvlTax
            {
                InstitutionInfoId = (int)id
            };
            return View(data);
        }

        // POST: LocalLvlTaxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,InvoiceNumber,VatRegNo,Blink,DateTime,Price,Vat,InstitutionInfoId")] LocalLvlTax localLvlTax)
        {
            if (ModelState.IsValid)
            {
                db.LocalLvlTaxs.Add(localLvlTax);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new {id = localLvlTax.InstitutionInfoId});
            }

            if (localLvlTax.InstitutionInfoId != 0)
            {
                var data = new LocalLvlTax
                {
                    InstitutionInfoId = localLvlTax.InstitutionInfoId
                };
                return View(data);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: LocalLvlTaxes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocalLvlTax localLvlTax = await db.LocalLvlTaxs.FindAsync(id);
            if (localLvlTax == null)
            {
                return HttpNotFound();
            }
            return View(localLvlTax);
        }

        // POST: LocalLvlTaxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,InvoiceNumber,VatRegNo,Blink,DateTime,Price,Vat,InstitutionInfoId")] LocalLvlTax localLvlTax)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localLvlTax).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new {id = localLvlTax.InstitutionInfoId});
            }
            return View(localLvlTax);
        }

        // GET: LocalLvlTaxes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocalLvlTax localLvlTax = await db.LocalLvlTaxs.FindAsync(id);
            if (localLvlTax == null)
            {
                return HttpNotFound();
            }
            return View(localLvlTax);
        }

        // POST: LocalLvlTaxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LocalLvlTax localLvlTax = await db.LocalLvlTaxs.FindAsync(id);
            if (localLvlTax != null) db.LocalLvlTaxs.Remove(localLvlTax);
            await db.SaveChangesAsync();
            if (localLvlTax != null) return RedirectToAction("Index", new {id = localLvlTax.InstitutionInfoId});
            return HttpNotFound();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
