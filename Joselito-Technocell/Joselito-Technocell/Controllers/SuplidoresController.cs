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
    public class SuplidoresController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();

        // GET: Suplidores
        public async Task<ActionResult> Index()
        {
            return View(await db.Suplidors.ToListAsync());
        }

        // GET: Suplidores/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suplidor suplidor = await db.Suplidors.FindAsync(id);
            if (suplidor == null)
            {
                return HttpNotFound();
            }
            return View(suplidor);
        }

        // GET: Suplidores/Create
        public ActionResult Create()
        {
            return View(new Suplidor());
        }

        // POST: Suplidores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SuplidorId,SuplidorNombre,SuplidorDireccion1,Paginaweb,Email,Telefono1,Telefono2,Observacion,Direccion2")] Suplidor suplidor)
        {
            if (ModelState.IsValid)
            {
                db.Suplidors.Add(suplidor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(suplidor);
        }

        // GET: Suplidores/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suplidor suplidor = await db.Suplidors.FindAsync(id);
            if (suplidor == null)
            {
                return HttpNotFound();
            }
            return View(suplidor);
        }

        // POST: Suplidores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SuplidorId,SuplidorNombre,SuplidorDireccion1,Paginaweb,Email,Telefono1,Telefono2,Observacion,Direccion2")] Suplidor suplidor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suplidor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(suplidor);
        }

        // GET: Suplidores/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suplidor suplidor = await db.Suplidors.FindAsync(id);
            if (suplidor == null)
            {
                return HttpNotFound();
            }
            return View(suplidor);
        }

        // POST: Suplidores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Suplidor suplidor = await db.Suplidors.FindAsync(id);
            db.Suplidors.Remove(suplidor);
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
