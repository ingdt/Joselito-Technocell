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
    public class RequisicionesController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();

        // GET: Requisiciones
        public async Task<ActionResult> Index()
        {
            var requisitions = db.Requisitions.Include(r => r.Suplidor);
            return View(await requisitions.ToListAsync());
        }

        // GET: Requisiciones/Details/5
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

        [HttpPost]
        public ActionResult addProduct(int? idProducto, int? cantidad, decimal? precio)
        {
            var requisition = Session["requisition"] as Requisitions;

            if (idProducto > 0 && cantidad > 0 && precio > 0)
            {
                bool has = false;

                foreach (var item in requisition.Detalle)
                {
                    if (item.productId == idProducto)
                    {
                        item.Cantidad += (int)cantidad;
                        item.Precio = (decimal)precio;
                        has = true;
                    }
                }

                if (!has)
                {
                    requisition.Detalle.Add(new DetalleRequisition
                    {
                        Cantidad = (int)cantidad,
                        productId = (int)idProducto,
                        Precio = (decimal)precio,
                        Unidad = 1
                    });
                }


                Session["requisition"] = requisition;
            }


            ViewBag.SuplidorId = new SelectList(db.Suplidors, "SuplidorId", "SuplidorNombre");
            return View(nameof(Create), requisition);
        }

        // GET: Requisiciones/Create
        public ActionResult Create()
        {
            Requisitions requisition = new Requisitions();
            requisition.FechadePedido = DateTime.Now;
            requisition.Detalle = new List<DetalleRequisition>();
            requisition.Estado = EstadoRequisicion.Creada;

            Session["requisition"] = requisition;
            ViewBag.SuplidorId = new SelectList(db.Suplidors, "SuplidorId", "SuplidorNombre");
            var products = db.Products.OrderBy(a => a.Name);
            ViewBag.Products = products;


            return View(requisition);
        }

        // POST: Requisiciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Requisitions requisitions)
        {
            var sesionRequisitions = Session["requisition"] as Requisitions;


            if (ModelState.IsValid)
            {
                using (var trann = db.Database.BeginTransaction())
                {
                    try
                    {
                        requisitions.Estado = EstadoRequisicion.Creada;
                        db.Requisitions.Add(requisitions);
                        await db.SaveChangesAsync();

                        foreach (var item in sesionRequisitions.Detalle)
                        {
                            item.RequisitionsId = requisitions.RequisitionsId;
                            item.Product = null;
                            db.DetalleRequisitions.Add(item);
                        }

                        await db.SaveChangesAsync();

                        trann.Commit();
                    }
                    catch (Exception ex)
                    {
                        trann.Rollback();
                    }
                }

                Session["requisition"] = null;

                return RedirectToAction("Index");
            }

            ViewBag.SuplidorId = new SelectList(db.Suplidors, "SuplidorId", "SuplidorNombre", requisitions.SuplidorId);

            requisitions = sesionRequisitions;

            return View(requisitions);
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
