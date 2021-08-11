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
    public class RegistroAlmacensController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();

        // GET: RegistroAlmacens
        public async Task<ActionResult> Index()
        {
            var registroAlmacens = db.registroAlmacens.Include(r => r.Anaquel);
            return View(await registroAlmacens.ToListAsync());
        }

        // GET: RegistroAlmacens/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroAlmacen registroAlmacen = await db.registroAlmacens.FindAsync(id);
            if (registroAlmacen == null)
            {
                return HttpNotFound();
            }
            return View(registroAlmacen);
        }

        // GET: RegistroAlmacens/Create
        public ActionResult Create()
        {
            ViewBag.AnaquelId = new SelectList(db.Anaquels, "AnaquelId", "Description");
            return View();
        }

        // POST: RegistroAlmacens/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RegistroAlmacenId,ProductosId,AnaquelId,Cantidad,TipoUnidadMedida,FechaRegistro")] RegistroAlmacen registroAlmacen)
        {
            if (ModelState.IsValid)
            {
                db.registroAlmacens.Add(registroAlmacen);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AnaquelId = new SelectList(db.Anaquels, "AnaquelId", "Description", registroAlmacen.AnaquelId);
            return View(registroAlmacen);
        }

        // GET: RegistroAlmacens/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroAlmacen registroAlmacen = await db.registroAlmacens.FindAsync(id);
            if (registroAlmacen == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnaquelId = new SelectList(db.Anaquels, "AnaquelId", "Description", registroAlmacen.AnaquelId);
            return View(registroAlmacen);
        }

        // POST: RegistroAlmacens/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RegistroAlmacenId,ProductosId,AnaquelId,Cantidad,TipoUnidadMedida,FechaRegistro")] RegistroAlmacen registroAlmacen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registroAlmacen).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AnaquelId = new SelectList(db.Anaquels, "AnaquelId", "Description", registroAlmacen.AnaquelId);
            return View(registroAlmacen);
        }

        // GET: RegistroAlmacens/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroAlmacen registroAlmacen = await db.registroAlmacens.FindAsync(id);
            if (registroAlmacen == null)
            {
                return HttpNotFound();
            }
            return View(registroAlmacen);
        }

        // POST: RegistroAlmacens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RegistroAlmacen registroAlmacen = await db.registroAlmacens.FindAsync(id);
            db.registroAlmacens.Remove(registroAlmacen);
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
