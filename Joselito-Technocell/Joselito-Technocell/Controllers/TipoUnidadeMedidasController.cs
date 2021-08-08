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
    public class TipoUnidadeMedidasController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();

        // GET: TipoUnidadeMedidas
        public async Task<ActionResult> Index()
        {
            return View(await db.TipoUnidadesategories.ToListAsync());
        }

        // GET: TipoUnidadeMedidas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoUnidadeMedidas tipoUnidadeMedidas = await db.TipoUnidadesategories.FindAsync(id);
            if (tipoUnidadeMedidas == null)
            {
                return HttpNotFound();
            }
            return View(tipoUnidadeMedidas);
        }

        // GET: TipoUnidadeMedidas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoUnidadeMedidas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TipoUnidadeMedidasId,Name,Description,Simbol")] TipoUnidadeMedidas tipoUnidadeMedidas)
        {
            if (ModelState.IsValid)
            {
                db.TipoUnidadesategories.Add(tipoUnidadeMedidas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoUnidadeMedidas);
        }

        // GET: TipoUnidadeMedidas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoUnidadeMedidas tipoUnidadeMedidas = await db.TipoUnidadesategories.FindAsync(id);
            if (tipoUnidadeMedidas == null)
            {
                return HttpNotFound();
            }
            return View(tipoUnidadeMedidas);
        }

        // POST: TipoUnidadeMedidas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TipoUnidadeMedidasId,Name,Description,Simbol")] TipoUnidadeMedidas tipoUnidadeMedidas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoUnidadeMedidas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoUnidadeMedidas);
        }

        // GET: TipoUnidadeMedidas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoUnidadeMedidas tipoUnidadeMedidas = await db.TipoUnidadesategories.FindAsync(id);
            if (tipoUnidadeMedidas == null)
            {
                return HttpNotFound();
            }
            return View(tipoUnidadeMedidas);
        }

        // POST: TipoUnidadeMedidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TipoUnidadeMedidas tipoUnidadeMedidas = await db.TipoUnidadesategories.FindAsync(id);
            db.TipoUnidadesategories.Remove(tipoUnidadeMedidas);
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
