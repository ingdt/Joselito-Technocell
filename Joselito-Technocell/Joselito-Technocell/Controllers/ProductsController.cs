using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Joselito_Technocell.Models;
using System;

namespace Joselito_Technocell.Controllers
{
    public class ProductsController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();

        // GET: Products
        public async Task<ActionResult> Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(await products.ToListAsync());
        }

        // GET: Products/Details/5
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

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {

                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
                return View(product);
            }
            return RedirectToAction("Index");
        }

        // GET: Products/Edit/5
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
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(product).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
                return View(product);
            }
            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);
            try
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return View(product);
                throw;
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
