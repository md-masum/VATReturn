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
    [Authorize]
    public class TreasuriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Treasuries
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

            var treasuries = db.Treasuries.Include(t => t.InstitutionInfo).Where(c => c.InstitutionInfoId == id && (c.DateTime <= endDate && c.DateTime >= startDate));

            if (!treasuries.Any())
            {
                ViewBag.massage = (int)id;
            }

            return View(await treasuries.ToListAsync());
        }

        // GET: Treasuries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treasury treasury = await db.Treasuries.FindAsync(id);
            if (treasury == null)
            {
                return HttpNotFound();
            }
            return View(treasury);
        }

        // GET: Treasuries/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }



            if (id == 0)
                return HttpNotFound();

            var data = new Treasury
            {
                InstitutionInfoId = (int)id
            };
            return View(data);
        }

        // POST: Treasuries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TreasuryNo,DateTime,Bank,Branch,Quantity,InstitutionInfoId")] Treasury treasury)
        {
            if (ModelState.IsValid)
            {
                db.Treasuries.Add(treasury);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = treasury.InstitutionInfoId});
            }

            if (treasury.InstitutionInfoId != 0)
            {
                var data = new Treasury
                {
                    InstitutionInfoId = treasury.InstitutionInfoId
                };
                return View(data);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Treasuries/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treasury treasury = await db.Treasuries.FindAsync(id);
            if (treasury == null)
            {
                return HttpNotFound();
            }

            return View(treasury);
        }

        // POST: Treasuries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TreasuryNo,DateTime,Bank,Branch,Quantity,InstitutionInfoId")] Treasury treasury)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treasury).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new {id = treasury.InstitutionInfoId});
            }
            return View(treasury);
        }

        // GET: Treasuries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treasury treasury = await db.Treasuries.FindAsync(id);
            if (treasury == null)
            {
                return HttpNotFound();
            }
            return View(treasury);
        }

        // POST: Treasuries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Treasury treasury = await db.Treasuries.FindAsync(id);
            if (treasury != null) db.Treasuries.Remove(treasury);
            await db.SaveChangesAsync();
            if (treasury != null) return RedirectToAction("Index", new {id = treasury.InstitutionInfoId});
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
