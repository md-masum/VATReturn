using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using VATReturn.Models;

namespace VATReturn.Controllers
{
    public class RebateExportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RebateExports
        public async Task<ActionResult> Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var rebateExports = db.RebateExports.Include(r => r.InstitutionInfo).Where(c => c.InstitutionInfoId == id);

            if (!rebateExports.Any())
            {
                ViewBag.massage = (int)id;
            }

            return View(await rebateExports.ToListAsync());
        }

        // GET: RebateExports/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RebateExport rebateExport = await db.RebateExports.FindAsync(id);
            if (rebateExport == null)
            {
                return HttpNotFound();
            }
            return View(rebateExport);
        }

        // GET: RebateExports/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == 0)
                return HttpNotFound();

            var data = new RebateExport
            {
                InstitutionInfoId = (int)id
            };
            return View(data);
        }

        // POST: RebateExports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CustomsDuty,RegulatoryDuties,SupplementaryDuty,InstitutionInfoId")] RebateExport rebateExport)
        {
            if (ModelState.IsValid)
            {
                db.RebateExports.Add(rebateExport);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new {id = rebateExport.InstitutionInfoId});
            }

            if (rebateExport.InstitutionInfoId != 0)
            {
                var data = new RebateExport
                {
                    InstitutionInfoId = rebateExport.InstitutionInfoId
                };
                return View(data);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: RebateExports/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RebateExport rebateExport = await db.RebateExports.FindAsync(id);
            if (rebateExport == null)
            {
                return HttpNotFound();
            }
            return View(rebateExport);
        }

        // POST: RebateExports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CustomsDuty,RegulatoryDuties,SupplementaryDuty,InstitutionInfoId")] RebateExport rebateExport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rebateExport).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new {id = rebateExport.InstitutionInfoId});
            }
            return View(rebateExport);
        }

        // GET: RebateExports/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RebateExport rebateExport = await db.RebateExports.FindAsync(id);
            if (rebateExport == null)
            {
                return HttpNotFound();
            }
            return View(rebateExport);
        }

        // POST: RebateExports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RebateExport rebateExport = await db.RebateExports.FindAsync(id);
            if (rebateExport != null) db.RebateExports.Remove(rebateExport);
            await db.SaveChangesAsync();
            if (rebateExport != null) return RedirectToAction("Index", new {id = rebateExport.InstitutionInfoId});
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
