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
using Joselito_Technocell.Helpers;

namespace Joselito_Technocell.Controllers
{
    public class ProductsController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();

        public async Task<ActionResult> Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Company).Include(p => p.Tax);
            return View(await products.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(Helper.GetCategories(), "CategoryId", "Name");
            ViewBag.CompanyId = new SelectList(Helper.GetCompanies(), "CompanyId", "Name");
            ViewBag.TaxId = new SelectList(Helper.GetTaxes(), "TaxId", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(Helper.GetCategories(), "CategoryId", "Name", product.CategoryId);
            ViewBag.CompanyId = new SelectList(Helper.GetCompanies(), "CompanyId", "Name", product.CompanyId);
            ViewBag.TaxId = new SelectList(Helper.GetTaxes(), "TaxId", "Description", product.TaxId);
            return View(product);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(Helper.GetCategories(), "CategoryId", "Name", product.CategoryId);
            ViewBag.CompanyId = new SelectList(Helper.GetCompanies(), "CompanyId", "Name", product.CompanyId);
            ViewBag.TaxId = new SelectList(Helper.GetTaxes(), "TaxId", "Description", product.TaxId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(Helper.GetCategories(), "CategoryId", "Name", product.CategoryId);
            ViewBag.CompanyId = new SelectList(Helper.GetCompanies(), "CompanyId", "Name", product.CompanyId);
            ViewBag.TaxId = new SelectList(Helper.GetTaxes(), "TaxId", "Description", product.TaxId);
            return View(product);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
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
