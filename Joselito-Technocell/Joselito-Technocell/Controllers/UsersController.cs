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
    public class UsersController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(Helper.GetCities(), "CityId", "Name");
            ViewBag.CompanyId = new SelectList(Helper.GetCompanies(), "CompanyId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                try
                {
                    await db.SaveChangesAsync();
                    UserHelper.CreateUserASP(user.UserName, "User");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                                        ex.InnerException.InnerException != null &&
                                        ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "The are record with the same value");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }
            ViewBag.CityId = new SelectList(Helper.GetCities(), "CityId", "Name", user.CityId);
            ViewBag.CompanyId = new SelectList(Helper.GetCompanies(), "CompanyId", "Name", user.CityId);
            return View(user);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(Helper.GetCities(), "CityId", "Name", user.CityId);
            ViewBag.CompanyId = new SelectList(Helper.GetCompanies(), "CompanyId", "Name", user.CityId);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Photo != null)
                {
                    var folder = "~/Content/UserFile";
                    var fileResponse = Helper.UploadPhoto(user.PhotoFile, folder, string.Format("{0}.jpg", user.UserId));
                    if (fileResponse)
                    {
                        var pic = string.Format("{0}/{1}.jpg", folder, user.UserId);
                        user.Photo = pic;
                    }
                }
                db.Entry(user).State = EntityState.Modified;
                var db2 = new Joselito_TechnocellDbContext();
                var currentUser = db2.Users.Find(user.UserId);
                if (currentUser.UserName != user.UserName)
                {
                    UserHelper.UpdateUserName(currentUser.UserName, user.UserName);
                }
                db2.Dispose(); 
                try
                {
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                                        ex.InnerException.InnerException != null &&
                                        ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "The are record with the same value");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }
            ViewBag.CityId = new SelectList(Helper.GetCities(), "CityId", "Name", user.CityId);
            ViewBag.CompanyId = new SelectList(Helper.GetCompanies(), "CompanyId", "Name", user.CityId);

            return View(user);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            try
            {
                UserHelper.DeleteUser(user.UserName);
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
            return View(user);
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
