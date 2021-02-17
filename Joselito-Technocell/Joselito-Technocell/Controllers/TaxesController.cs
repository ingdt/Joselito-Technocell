using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Joselito_Technocell.Models;

namespace Joselito_Technocell.Controllers
{
    public class TaxesController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();

        // GET: Taxes
        public async Task<ActionResult> Index()
        {
            return View(await db.Taxes.ToListAsync());
        }

        // GET: Taxes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tax tax = await db.Taxes.FindAsync(id);
            if (tax == null)
            {
                return HttpNotFound();
            }
            return View(tax);
        }

        // GET: Taxes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Taxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TaxId,Description,Rate,CompanyId")] Tax tax)
        {
            if (ModelState.IsValid)
            {
                db.Taxes.Add(tax);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tax);
        }

        // GET: Taxes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tax tax = await db.Taxes.FindAsync(id);
            if (tax == null)
            {
                return HttpNotFound();
            }
            return View(tax);
        }

        // POST: Taxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TaxId,Description,Rate,CompanyId")] Tax tax)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tax).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tax);
        }

        // GET: Taxes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tax tax = await db.Taxes.FindAsync(id);
            if (tax == null)
            {
                return HttpNotFound();
            }
            return View(tax);
        }

        // POST: Taxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tax tax = await db.Taxes.FindAsync(id);
            db.Taxes.Remove(tax);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
