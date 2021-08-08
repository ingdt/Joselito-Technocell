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
    public class RequisitionsController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();

        // GET: Requisitions
        public async Task<ActionResult> Index()
        {
            var requisitions = db.Requisitions.Include(r => r.Deparment);
            return View(await requisitions.ToListAsync());
        }

        // GET: Requisitions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisitions requisitions = await db.Requisitions.FindAsync(id);
            if (requisitions == null)
            {
                return HttpNotFound();
            }
            return View(requisitions);
        }

        // GET: Requisitions/Create
        public ActionResult Create()
        {
            ViewBag.DeparmentId = new SelectList(db.Deparments, "DeparmentId", "Descripcion");
            return View();
        }

        // POST: Requisitions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RequisitionsId,FechadePedido,FechadeEntrega,Cantidad,Unidad,Articulo,DeparmentId")] Requisitions requisitions)
        {
            if (ModelState.IsValid)
            {
                db.Requisitions.Add(requisitions);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DeparmentId = new SelectList(db.Deparments, "DeparmentId", "Descripcion", requisitions.DeparmentId);
            return View(requisitions);
        }

        // GET: Requisitions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisitions requisitions = await db.Requisitions.FindAsync(id);
            if (requisitions == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeparmentId = new SelectList(db.Deparments, "DeparmentId", "Descripcion", requisitions.DeparmentId);
            return View(requisitions);
        }

        // POST: Requisitions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RequisitionsId,FechadePedido,FechadeEntrega,Cantidad,Unidad,Articulo,DeparmentId")] Requisitions requisitions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requisitions).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DeparmentId = new SelectList(db.Deparments, "DeparmentId", "Descripcion", requisitions.DeparmentId);
            return View(requisitions);
        }

        // GET: Requisitions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisitions requisitions = await db.Requisitions.FindAsync(id);
            if (requisitions == null)
            {
                return HttpNotFound();
            }
            return View(requisitions);
        }

        // POST: Requisitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Requisitions requisitions = await db.Requisitions.FindAsync(id);
            db.Requisitions.Remove(requisitions);
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
