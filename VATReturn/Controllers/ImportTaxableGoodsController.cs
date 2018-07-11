using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using VATReturn.Models;
using System;

namespace VATReturn.Controllers
{
    [Authorize]
    public class ImportTaxableGoodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ImportTaxableGoods
        public async Task<ActionResult> Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var valueAddedTax = Session["ValueAddedTax"] as ValueAddedTax;
            if (valueAddedTax == null)
            {
                return HttpNotFound();
            }

            DateTime now = valueAddedTax.Date.GetValueOrDefault();
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var importTaxableGoodses = db.ImportTaxableGoodses.Include(i => i.InstitutionInfo).Where(c => c.InstitutionInfoId == id && (c.DateTime <= endDate && c.DateTime >= startDate));
            if (!importTaxableGoodses.Any())
            {
                ViewBag.massage = (int)id;
            }
            return View(await importTaxableGoodses.ToListAsync());
        }

        // GET: ImportTaxableGoods/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportTaxableGoods importTaxableGoods = await db.ImportTaxableGoodses.FindAsync(id);
            if (importTaxableGoods == null)
            {
                return HttpNotFound();
            }
            return View(importTaxableGoods);
        }

        // GET: ImportTaxableGoods/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == 0)
                return HttpNotFound();

            var data = new ImportTaxableGoods
            {
                InstitutionInfoId = (int)id
            };
            return View(data);
        }

        // POST: ImportTaxableGoods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,BandENo,DateTime,Price,Vat,InstitutionInfoId")] ImportTaxableGoods importTaxableGoods)
        {
            if (ModelState.IsValid)
            {
                db.ImportTaxableGoodses.Add(importTaxableGoods);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new {id = importTaxableGoods.InstitutionInfoId});
            }

            if (importTaxableGoods.InstitutionInfoId != 0)
            {
                var data = new ImportTaxableGoods
                {
                    InstitutionInfoId = importTaxableGoods.InstitutionInfoId
                };
                return View(data);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: ImportTaxableGoods/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportTaxableGoods importTaxableGoods = await db.ImportTaxableGoodses.FindAsync(id);
            if (importTaxableGoods == null)
            {
                return HttpNotFound();
            }
            return View(importTaxableGoods);
        }

        // POST: ImportTaxableGoods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,BandENo,DateTime,Price,Vat,InstitutionInfoId")] ImportTaxableGoods importTaxableGoods)
        {
            if (ModelState.IsValid)
            {
                db.Entry(importTaxableGoods).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new {id = importTaxableGoods.InstitutionInfoId});
            }
            return View(importTaxableGoods);
        }

        // GET: ImportTaxableGoods/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImportTaxableGoods importTaxableGoods = await db.ImportTaxableGoodses.FindAsync(id);
            if (importTaxableGoods == null)
            {
                return HttpNotFound();
            }
            return View(importTaxableGoods);
        }

        // POST: ImportTaxableGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ImportTaxableGoods importTaxableGoods = await db.ImportTaxableGoodses.FindAsync(id);
            if (importTaxableGoods != null)
            {
                db.ImportTaxableGoodses.Remove(importTaxableGoods);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new {id = importTaxableGoods.InstitutionInfoId});
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
