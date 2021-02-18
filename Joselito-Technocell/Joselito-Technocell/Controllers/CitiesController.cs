using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Joselito_Technocell.Helpers;
using Joselito_Technocell.Models;

namespace Joselito_Technocell.Controllers
{
    public class CitiesController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();


        public async Task<ActionResult> Index()
        {
            var cities = await db.Cities.Include(c => c.Department).ToListAsync();
           return View(cities); 
        }


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }


        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments.OrderBy(d=> d.Name), "DepartmentId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<ActionResult> Create(City city)
        {
            if (ModelState.IsValid)
            {
                db.Cities.Add(city);
                try
                {
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                                        ex.InnerException.InnerException != null &&
                                        ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                    {
                        ModelState.AddModelError(string.Empty, "The record can't be delete beacuse it has related record");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }

            ViewBag.DepartmentId = new SelectList(Helper.GetDepartments(), "DepartmentId", "Name", city.DepartmentId);
            return View(city);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(Helper.GetDepartments(), "DepartmentId", "Name", city.DepartmentId);
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( City city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                try
                {
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                                        ex.InnerException.InnerException != null &&
                                        ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                    {
                        ModelState.AddModelError(string.Empty, "The record can't be delete beacuse it has related record");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }
            ViewBag.DepartmentId = new SelectList(Helper.GetDepartments(), "DepartmentId", "Name", city.DepartmentId);
            return View(city);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = await db.Cities.FindAsync(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            City city = await db.Cities.FindAsync(id);
            db.Cities.Remove(city);
            try
            {
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                                    ex.InnerException.InnerException != null &&
                                    ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    ModelState.AddModelError(string.Empty, "The record can't be delete beacuse it has related record");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(city);
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
