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
    public class CustomersController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();

        // GET: Customers
        public async Task<ActionResult> Index()
        {
            var customers = db.Customers.Include(c => c.City).Include(c => c.Department);
            return View(await customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CustomerId,UserName,FirstName,LastName,Photo,Phone,Address,Latitude,Longitude,DepartmentId,CityId,IsUpdated")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", customer.CityId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", customer.DepartmentId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", customer.CityId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", customer.DepartmentId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CustomerId,UserName,FirstName,LastName,Photo,Phone,Address,Latitude,Longitude,DepartmentId,CityId,IsUpdated")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "Name", customer.CityId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", customer.DepartmentId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            db.Customers.Remove(customer);
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
