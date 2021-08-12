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

        // GET: RegistroAlmacens1
        public async Task<ActionResult> Index()
        {
            var registroAlmacens = db.registroAlmacens.Include(r => r.Anaquel);
            return View(await registroAlmacens.ToListAsync());
        }

        // GET: RegistroAlmacens1/Details/5
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

        // GET: RegistroAlmacens1/Create
        public ActionResult Create()
        {
            ViewBag.AnaquelId = new SelectList(db.Anaquels, "AnaquelId", "Description");
            return View();
        }

        // POST: RegistroAlmacens1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegistroAlmacen registroAlmacen)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var lisRegistro = new List<RegistroAlmacen>();
                    for (int i = 0; i < registroAlmacen.Cantidad; i++)
                    {
                        registroAlmacen.EntradaSalida = true;
                        registroAlmacen.codigo = String.Format("{0}-{1}", registroAlmacen.Product.Name,registroAlmacen.AnaquelId,db.registroAlmacens.Count());
                        db.registroAlmacens.Add(registroAlmacen);
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch
            {
                ViewBag.AnaquelId = new SelectList(db.Anaquels, "AnaquelId", "Description", registroAlmacen.AnaquelId);
                return View(registroAlmacen);
            }
            return RedirectToAction("Index");
        }
        // GET: RegistroAlmacens1/Edit/5
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

        // POST: RegistroAlmacens1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RegistroAlmacen registroAlmacen)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(registroAlmacen).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                ViewBag.AnaquelId = new SelectList(db.Anaquels, "AnaquelId", "Description", registroAlmacen.AnaquelId);
                return View(registroAlmacen);
            }
            return RedirectToAction("Index");
        }


        // GET: RegistroAlmacens1/Edit/5
        public async Task<ActionResult> Salida(int? id)
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

        // POST: RegistroAlmacens1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Salida(RegistroAlmacen registroAlmacen)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(registroAlmacen).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                ViewBag.AnaquelId = new SelectList(db.Anaquels, "AnaquelId", "Description", registroAlmacen.AnaquelId);
                return View(registroAlmacen);
            }
            return RedirectToAction("Index");
        }

        // GET: RegistroAlmacens1/Delete/5
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

        // POST: RegistroAlmacens1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RegistroAlmacen registroAlmacen = await db.registroAlmacens.FindAsync(id);
            try
            {
                db.registroAlmacens.Remove(registroAlmacen);
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return View(registroAlmacen);
            }
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
