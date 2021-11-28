using Joselito_Technocell.Helpers;
using Joselito_Technocell.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Joselito_Technocell.Controllers
{
    [Authorize(Roles = "AD-ROOT, AU-CONT")]
    public class CxCController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();

        // GET: CxC
        public async Task<ActionResult> Index()
        {
            var applicationDbContext = await db.CxC.ToListAsync();

            var lista = new List<Cliente>();

            foreach (var item in applicationDbContext)
            {
                var client = lista.FirstOrDefault(a => a.IdCliente == item.IdCliente);

                if (client == null)
                {
                    client = db.Clientes.Find(item.IdCliente);
                    lista.Add(client);
                }
            }

            foreach (var item in lista)
            {
                item.CxC = db.CxC.Where(a => a.IdCliente == item.IdCliente).ToList();
            }

            return View(lista);
        }

        public async Task<ActionResult> ClienteCxC(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            return View(await db.CxC.Where(a => a.IdCliente == id).Include(a=> a.Cliente).ToListAsync());
        }

        public async Task<ActionResult> PagosCxC(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pagos = await db.PagosCxC.Where(a=> a.IdCxC == id).ToListAsync();

            return View(pagos);
        }

        public async Task<ActionResult> print()
        {
            var applicationDbContext = await db.CxC.Where(a => a.Saldado == false).ToListAsync();

            var lista = new List<Cliente>();

            foreach (var item in applicationDbContext)
            {
                var client = lista.FirstOrDefault(a => a.IdCliente == item.IdCliente);

                if (client == null)
                {
                    client = db.Clientes.Find(item.IdCliente);
                    lista.Add(client);
                }
            }

            foreach (var item in lista)
            {
                item.CxC = db.CxC.Where(a => a.IdCliente == item.IdCliente).ToList();
            }

            return View(lista);
        }
    }
}