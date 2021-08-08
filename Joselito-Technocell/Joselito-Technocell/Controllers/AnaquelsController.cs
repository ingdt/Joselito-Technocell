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
    public class AnaquelsController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();

        // GET: Anaquels
        public async Task<ActionResult> Index()
        {
            return View(await db.Anaquels.ToListAsync());
        }

        // GET: Anaquels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anaquel anaquel = await db.Anaquels.FindAsync(id);
            if (anaquel == null)
            {
                return HttpNotFound();
            }
            return View(anaquel);
        }

        // GET: Anaquels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Anaquels/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AnaquelId,Description,InventotyId")] Anaquel anaquel)
        {
            if (ModelState.IsValid)
            {
                db.Anaquels.Add(anaquel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(anaquel);
        }

        // GET: Anaquels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anaquel anaquel = await db.Anaquels.FindAsync(id);
            if (anaquel == null)
            {
                return HttpNotFound();
            }
            return View(anaquel);
        }

        // POST: Anaquels/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AnaquelId,Description,InventotyId")] Anaquel anaquel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anaquel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(anaquel);
        }

        // GET: Anaquels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anaquel anaquel = await db.Anaquels.FindAsync(id);
            if (anaquel == null)
            {
                return HttpNotFound();
            }
            return View(anaquel);
        }

        // POST: Anaquels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Anaquel anaquel = await db.Anaquels.FindAsync(id);
            db.Anaquels.Remove(anaquel);
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
