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
    [Authorize]
    public class InventarioController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();

        // GET: Inventario
        public async Task<ActionResult> Index()
        {
            var inventarios = db.Inventarios.Include(i => i.Producto);
            return View(await inventarios.ToListAsync());
        }

        public async Task<ActionResult> print()
        {
            var inventarios = db.Inventarios.Include(i => i.Producto);
            return View(await inventarios.ToListAsync());
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
