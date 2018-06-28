using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using VATReturn.Models;

namespace VATReturn.Controllers
{
    public class OtherCoordinationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OtherCoordinations
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

            var otherCoordinations = db.OtherCoordinations.Include(o => o.InstitutionInfo).Where(c => c.InstitutionInfoId == id && (c.DateTime <= endDate && c.DateTime >= startDate));

            if (!otherCoordinations.Any())
            {
                ViewBag.massage = (int)id;
            }

            return View(await otherCoordinations.ToListAsync());
        }

        // GET: OtherCoordinations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtherCoordination otherCoordination = await db.OtherCoordinations.FindAsync(id);
            if (otherCoordination == null)
            {
                return HttpNotFound();
            }
            return View(otherCoordination);
        }

        // GET: OtherCoordinations/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == 0)
                return HttpNotFound();

            var data = new OtherCoordination
            {
                InstitutionInfoId = (int)id
            };
            return View(data);
        }

        // POST: OtherCoordinations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,OtherRebates,Owing,SourceCut,Blank,DateTime,InstitutionInfoId")] OtherCoordination otherCoordination)
        {
            if (ModelState.IsValid)
            {
                db.OtherCoordinations.Add(otherCoordination);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new {id = otherCoordination.InstitutionInfoId});
            }

            if (otherCoordination.InstitutionInfoId != 0)
            {
                var data = new OtherCoordination
                {
                    InstitutionInfoId = otherCoordination.InstitutionInfoId
                };
                return View(data);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: OtherCoordinations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtherCoordination otherCoordination = await db.OtherCoordinations.FindAsync(id);
            if (otherCoordination == null)
            {
                return HttpNotFound();
            }
            return View(otherCoordination);
        }

        // POST: OtherCoordinations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,OtherRebates,Owing,SourceCut,Blank,DateTime,InstitutionInfoId")] OtherCoordination otherCoordination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(otherCoordination).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new {id = otherCoordination.InstitutionInfoId});
            }
            return View(otherCoordination);
        }

        // GET: OtherCoordinations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtherCoordination otherCoordination = await db.OtherCoordinations.FindAsync(id);
            if (otherCoordination == null)
            {
                return HttpNotFound();
            }
            return View(otherCoordination);
        }

        // POST: OtherCoordinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OtherCoordination otherCoordination = await db.OtherCoordinations.FindAsync(id);
            if (otherCoordination != null) db.OtherCoordinations.Remove(otherCoordination);
            await db.SaveChangesAsync();
            if (otherCoordination != null)
                return RedirectToAction("Index", new {id = otherCoordination.InstitutionInfoId});
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
