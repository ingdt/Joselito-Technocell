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
    [Authorize(Roles = "AD-ROOT, AD-ALMACEN")]
    public class DetalleRequisitionsController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();

        [ChildActionOnly]
        public async Task<ActionResult> Index(int? id)
        {
            var list = db.DetalleRequisitions.Where(c => c.RequisitionsId == id).ToList();
            return View(list);
        }
        // GET: DetalleRequisitions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleRequisition detalleRequisition = await db.DetalleRequisitions.FindAsync(id);
            if (detalleRequisition == null)
            {
                return HttpNotFound();
            }
            return View(detalleRequisition);
        }

        // GET: DetalleRequisitions/Create
        public ActionResult Create(int id)
        {
            var detalle = new DetalleRequisition();
            detalle.RequisitionsId = id;
            ViewBag.productId = new SelectList(db.Products, "ProductId", "Name");
            return View(detalle);
        }

        // POST: DetalleRequisitions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DetalleRequisition detalleRequisition)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.DetalleRequisitions.Add(detalleRequisition);
                    await db.SaveChangesAsync();
                }
                else
                {
                    ViewBag.productId = new SelectList(db.Products, "ProductId", "Name", detalleRequisition.productId);
                    return View(detalleRequisition);
                }
            }
            catch (Exception)
            {
                ViewBag.productId = new SelectList(db.Products, "ProductId", "Name", detalleRequisition.productId);
                return View(detalleRequisition);
            }
            return RedirectToAction("Index", "Requisitions");
        }

        // GET: DetalleRequisitions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleRequisition detalleRequisition = await db.DetalleRequisitions.FindAsync(id);
            if (detalleRequisition == null)
            {
                return HttpNotFound();
            }
            ViewBag.productId = new SelectList(db.Products, "ProductId", "Name", detalleRequisition.productId);
            ViewBag.RequisitionsId = new SelectList(db.Requisitions, "RequisitionsId", "RequisitionsId", detalleRequisition.RequisitionsId);
            return View(detalleRequisition);
        }

        // POST: DetalleRequisitions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DetalleRequisition detalleRequisition)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(detalleRequisition).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                ViewBag.productId = new SelectList(db.Products, "ProductId", "Name", detalleRequisition.productId);
                ViewBag.RequisitionsId = new SelectList(db.Requisitions, "RequisitionsId", "RequisitionsId", detalleRequisition.RequisitionsId);
                return View(detalleRequisition);
            }
            return RedirectToAction("Index");
        }

        // GET: DetalleRequisitions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleRequisition detalleRequisition = await db.DetalleRequisitions.FindAsync(id);
            if (detalleRequisition == null)
            {
                return HttpNotFound();
            }
            return View(detalleRequisition);
        }

        // POST: DetalleRequisitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DetalleRequisition detalleRequisition = await db.DetalleRequisitions.FindAsync(id);
            try
            {
                db.DetalleRequisitions.Remove(detalleRequisition);
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {

                return View(detalleRequisition);
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
