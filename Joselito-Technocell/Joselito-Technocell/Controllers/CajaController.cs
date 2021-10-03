using Joselito_Technocell.Helpers;
using Joselito_Technocell.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Joselito_Technocell.Controllers
{
    public class CajaController : Controller
    {
        private Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();

        // GET: Caja
        [Authorize]
        public ActionResult Index()
        {
            var uNAme = User.Identity.Name;
            var usu = UserHelper.User(uNAme);
            var caja = db.Cajas.FirstOrDefault(a=> a.OperadorId == usu.Id && a.Estdo == EstadoCaja.Abierta);

            if (caja == null)
            {
                return View("AbrirCaja", new Caja { OperadorId = usu.Id});
            }

            Factura factura = Session["factura"] as Factura;

            if (factura == null)
            {
                factura = new Factura
                {
                    DetalleFacturas = new List<DetalleFactura>(),
                };

                Session["factura"] = factura;
            }

            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "FullName", factura?.IdCliente);
            return View(factura);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(Factura factura)
        {
            var sesionFactura = Session["factura"] as Factura;
            var uNAme = User.Identity.Name;
            var usu = UserHelper.User(uNAme);
            factura.IdCaja = db.Cajas.FirstOrDefault(a=> a.OperadorId == usu.Id && a.Estdo == EstadoCaja.Abierta).CajaId;
            factura.Fecha = DateTime.Now;

            if (sesionFactura.Total > factura.Efectivo)
            {
                Session["error"] = $"La factura hace un total de {sesionFactura.Total} favor pagar con un monto mayor o equivalente";

                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                using (var trann = db.Database.BeginTransaction())
                {
                    try
                    {
                        factura.Estado = EstadoFactura.Pagada;
                        db.Facturas.Add(factura);
                        await db.SaveChangesAsync();

                        foreach (var item in sesionFactura.DetalleFacturas)
                        {
                            item.FacturaId = factura.FacturaId;
                            item.Product = null;
                            db.DetalleFacturas.Add(item);

                            var inventario = db.Inventarios.FirstOrDefault(a => a.ProductId == item.ProductId);

                            if (inventario == null || inventario.Cantidad < item.Cantidad)
                            {
                                Session["error"] = $"No tienes Stock suficiente para {item.Cantidad} del producto {inventario.Producto.Name}.";
                                ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "FullName");

                                trann.Rollback();
                                return RedirectToAction(nameof(Index));
                            }

                            inventario.Cantidad -= item.Cantidad;
                            db.Entry(inventario).State = EntityState.Modified;
                        }

                        await db.SaveChangesAsync();
                        trann.Commit();
                        Session["factura"] = null;

                        Session["ok"] = "Venta realizada con exito!";
                        Session["Imprimir"] = factura.FacturaId;
                    }
                    catch (Exception ex)
                    {
                        Session["error"] = ex.Message;
                        trann.Rollback();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Factura(int id)
        {
            return View(await db.Facturas.Include(a => a.DetalleFacturas).FirstOrDefaultAsync(a => a.FacturaId == id));
        }

        [Authorize]
        [HttpGet]
        public ActionResult AbrirCaja()
        {
            return View("AbrirCaja", new Caja());
        }

        [Authorize]
        [HttpPost]
        public ActionResult addProduct(int? idProducto, int? cantidad)
        {
            var factura = Session["factura"] as Factura;

            var inventario = db.Inventarios.Include(a => a.Producto).FirstOrDefault(a => a.ProductId == idProducto);

            if (inventario == null)
            {
                if (inventario.Cantidad < cantidad)
                {
                    Session["error"] = $"No tienes Stock suficiente para {cantidad} del producto {inventario.Producto.Name}.";
                    ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "FullName");
                    return RedirectToAction(nameof(Index));
                }

                Session["error"] = $"No tienes Stock suficiente para {cantidad} de este producto.";
                ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "FullName");
                return RedirectToAction(nameof(Index));

            }

            if (idProducto > 0 && cantidad > 0)
            {
                bool has = false;

                foreach (var item in factura.DetalleFacturas)
                {
                    if (item.ProductId == idProducto)
                    {
                        item.Cantidad += (int)cantidad;
                        has = true;
                    }
                }

                if (!has)
                {
                    var product = db.Products.Find(idProducto);
                    factura.DetalleFacturas.Add(new DetalleFactura
                    {
                        Cantidad = (int)cantidad,
                        ProductId = (int)idProducto,
                        Precio = product.Price,
                    });
                }


                Session["factura"] = factura;
            }


            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "FullName");
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        public ActionResult AbrirCaja(Caja caja)
        {
            caja.Apertura = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Cajas.Add(caja);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(caja);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CerrarCaja()
        {
            var uNAme = User.Identity.Name;
            var usu = UserHelper.User(uNAme);
            var caja = db.Cajas.FirstOrDefault(a => a.OperadorId == usu.Id && a.Estdo == EstadoCaja.Abierta);

            return View(caja);
        }

        [HttpGet]
        public ActionResult Cancelar()
        {
            Session.Remove("factura");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult DeleteProdut(int id)
        {
            var factura = Session["factura"] as Factura;

            var productToDelete = factura.DetalleFacturas.FirstOrDefault(a=> a.ProductId == id);

            if (productToDelete != null)
            {
                factura.DetalleFacturas.Remove(productToDelete);

                Session["factura"] = factura;
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        public ActionResult CerrarCaja(Caja caja)
        {
            caja.Apertura = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Cajas.Add(caja);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(caja);
        }
    }
}