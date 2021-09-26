using Joselito_Technocell.Helpers;
using Joselito_Technocell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "FullName");
            return View(factura);
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