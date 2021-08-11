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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Requisitions requisitions)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Requisitions.Add(requisitions);
                    await db.SaveChangesAsync();
                }
                else
                {
                    ViewBag.DeparmentId = new SelectList(db.Deparments, "DeparmentId", "Descripcion", requisitions.DeparmentId);
                    return View(requisitions);
                }

            }
            catch (Exception)
            {
                ViewBag.DeparmentId = new SelectList(db.Deparments, "DeparmentId", "Descripcion", requisitions.DeparmentId);
                return View(requisitions);
            }
            var id = db.Requisitions.OrderByDescending(d => d.DeparmentId).FirstOrDefault().RequisitionsId;
            return RedirectToAction("AddDetalle", id);

        }
        public async Task<ActionResult> AddDetalle(int? id)
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
            return RedirectToAction("Create", "DetalleRequisitions", new { id = id });
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Requisitions requisitions)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(requisitions).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                ViewBag.DeparmentId = new SelectList(db.Deparments, "DeparmentId", "Descripcion", requisitions.DeparmentId);
                return View(requisitions);
            }
            return RedirectToAction("Index");
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
            try
            {
                db.Requisitions.Remove(requisitions);
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return View(requisitions);
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
