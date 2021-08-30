using Joselito_Technocell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Joselito_Technocell.Controllers
{
    public class RequisicionesController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();
        // GET: Requisiciones
        public ActionResult Index()
        {
            ViewBag.SuplidorId = new SelectList(db.Suplidors, "SuplidorId", "SuplidorNombre");
            return View(new Requisitions { FechadeEntrega = DateTime.Today});
        }
    }
}