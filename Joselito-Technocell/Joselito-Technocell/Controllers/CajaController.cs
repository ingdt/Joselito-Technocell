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
            ViewBag.IdMetodoPago = new SelectList(db.MetodoPagos, "IdMetodoPago", "Descripcion", factura?.IdCliente);
            return View(factura);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(Factura factura)
        {
            var sesionFactura = Session["factura"] as Factura;
            sesionFactura.IdMetodoPago = factura.IdMetodoPago;
            sesionFactura.IdCliente = factura.IdCliente;
            sesionFactura.Efectivo = factura.Efectivo;
            Session["factura"] = sesionFactura;
            var uNAme = User.Identity.Name;
            var usu = UserHelper.User(uNAme);
            factura.IdCaja = db.Cajas.FirstOrDefault(a=> a.OperadorId == usu.Id && a.Estdo == EstadoCaja.Abierta).CajaId;
            factura.Fecha = DateTime.Now;

            if (sesionFactura.DetalleFacturas.Count == 0)
            {
                Session["error"] = $"No se puede realizar ventas sin seleccionar productos o servicios";

                return RedirectToAction(nameof(Index));
            }

            if (sesionFactura.Total > factura.Efectivo && factura.IdMetodoPago == 1)
            {
                Session["error"] = $"La factura hace un total de {sesionFactura.Total} favor pagar con un monto mayor o equivalente";

                return RedirectToAction(nameof(Index));
            }
            else if (factura.IdMetodoPago == 2 && (factura.IdCliente == 0 || factura.IdCliente == null))
            {
                Session["error"] = $"Favor seleccionar un cliente para realizar una venta a credito";

                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                using (var trann = db.Database.BeginTransaction())
                {
                    try
                    {
                        factura.Estado = factura.IdMetodoPago == 1 ? EstadoFactura.Pagada : EstadoFactura.Deuda;
                        db.Facturas.Add(factura);
                        await db.SaveChangesAsync();

                        //Crear asiento contable
                        var asiento = new AsientoContable
                        {
                            Fecha = DateTimeOffset.Now,
                            Glosa = $"Registro de venta #{factura.FacturaId}",
                            RazonSocial = factura.Cliente != null ? factura.Cliente.Nombre : "",
                            Tipo = Enum.TipoAsientoContable.Debito
                        };

                        db.AsientoContables.Add(asiento);
                        await db.SaveChangesAsync();

                        Cuenta cuenta = new Cuenta();

                        if (factura.IdMetodoPago == 1)
                        {
                            cuenta = await VerificarCuenta("Efectivo caja y banco");
                        }

                        else if (factura.IdMetodoPago == 2)
                        {
                            cuenta = await VerificarCuenta("Cuentas por cobrar");

                            var cxc = new CxC
                            {
                                IdCliente = factura.IdCliente,
                                Monto = sesionFactura.Total - factura.Efectivo,
                                Resto = sesionFactura.Total - factura.Efectivo,
                                FacturaId = factura.FacturaId,
                                Saldado = false,
                            };

                            db.CxC.Add(cxc);
                            await db.SaveChangesAsync();

                            var client = await db.Clientes.FindAsync(factura.IdCliente);
                            var detalleAsiento = new DetalleAsiento
                            {
                                IdAsientoContable = asiento.IdAsientoContable,
                                CuentaIdCuenta = cuenta.IdCuenta,
                                Debe = factura.Total - factura.Efectivo,
                                Hacer = 0,
                                Detalle = $"venta a credito a {client.Nombre}"
                            };

                            db.DetalleAsientosContables.Add(detalleAsiento);

                            if (factura.Efectivo > 0)
                            {
                                var c = await VerificarCuenta("Efectivo caja y banco ");

                                detalleAsiento = new DetalleAsiento
                                {
                                    IdAsientoContable = asiento.IdAsientoContable,
                                    CuentaIdCuenta = c.IdCuenta,
                                    Debe = factura.Efectivo,
                                    Hacer = 0,
                                    Detalle = $"venta a {db.Clientes.Find(factura.IdCliente).Nombre}"
                                };

                                db.DetalleAsientosContables.Add(detalleAsiento);

                                var ingreso = new Ingresos
                                {
                                    CajaId = (int)factura.IdCaja,
                                    CajaIdCaja = (int)factura.IdCaja,
                                    Fecha = DateTime.Now,
                                    FechaEmision = DateTime.Now,
                                    Observacion = $"venta a credito al cliente {db.Clientes.Find(factura.IdCliente).Nombre}",
                                    TotalIngreso = factura.Efectivo
                                };

                                db.Ingresos.Add(ingreso);
                            }

                        }

                        foreach (var item in sesionFactura.DetalleFacturas)
                        {
                            item.FacturaId = factura.FacturaId;
                            item.Product = null;
                            item.Factura = null;
                            db.DetalleFacturas.Add(item);

                            if (item.Product == null)
                            {
                                item.Product = db.Products.Find(item.ProductId);
                            }

                            var inventario = db.Inventarios.FirstOrDefault(a => a.ProductId == item.ProductId);
                            var producto = await db.Products.FindAsync(inventario.InventarioId);

                            if (inventario == null && !item.Product.IsService)
                            {
                                Session["error"] = $"No tienes Stock de este producto";
                                ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "FullName");
                                return RedirectToAction(nameof(Index));
                            }

                            else if (inventario != null)
                            {                               

                                if (inventario.Cantidad < item.Cantidad && !item.Product.IsService)
                                {
                                    Session["error"] = $"No tienes Stock suficiente para {item.Cantidad} del producto {inventario.Producto.Name}.";
                                    ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "FullName");
                                    return RedirectToAction(nameof(Index));
                                }
                            }

                            if (!item.Product.IsService)
                            {
                                inventario.Cantidad -= item.Cantidad;
                                db.Entry(inventario).State = EntityState.Modified;
                            }

                            if (factura.IdMetodoPago == 1)
                            {
                                var detalleAsiento = new DetalleAsiento
                                {
                                    IdAsientoContable = asiento.IdAsientoContable,
                                    CuentaIdCuenta = cuenta.IdCuenta,
                                    Debe = item.Total,
                                    Hacer = 0,
                                    Detalle = $"venta de {item.Cantidad} {item.Product.Name}"
                                };

                                db.DetalleAsientosContables.Add(detalleAsiento);

                                var ingreso = new Ingresos
                                {
                                    CajaId = (int)factura.IdCaja,
                                    CajaIdCaja = (int)factura.IdCaja,
                                    Fecha = DateTime.Now,
                                    FechaEmision = DateTime.Now,
                                    Observacion = $"venta de {item.Cantidad} {producto.Name}",
                                    TotalIngreso = item.Cantidad * item.Precio
                                };

                                db.Ingresos.Add(ingreso);
                            }
                        }

                        cuenta = await VerificarCuenta("Almacen");

                        var detalleAsiento1 = new DetalleAsiento
                        {
                            IdAsientoContable = asiento.IdAsientoContable,
                            CuentaIdCuenta = cuenta.IdCuenta,
                            Debe = 0,
                            Hacer = factura.Total,
                            Detalle = $"venta #{factura.FacturaId}"
                        };

                        db.DetalleAsientosContables.Add(detalleAsiento1);

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

        async Task<Cuenta> VerificarCuenta(string NombreCuenta)
        {
            var cuenta = await db.CuentasContables.FirstOrDefaultAsync(a => a.Descripcion == NombreCuenta);

            if (cuenta == null)
            {
                cuenta = new Cuenta { Descripcion = NombreCuenta,};
                db.CuentasContables.Add(cuenta);
                await db.SaveChangesAsync();
            }

            return cuenta;
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
            var producto = db.Products.Find(idProducto);

            if (producto == null)
            {
                Session["error"] = "el producto ya no existe";
                return RedirectToAction(nameof(Index));
            }

            var inventario = db.Inventarios.Include(a => a.Producto).FirstOrDefault(a => a.ProductId == idProducto);

            if (inventario == null && !producto.IsService)
            {
                Session["error"] = $"No tienes Stock de este producto";
                return RedirectToAction(nameof(Index));
            }

            else if (inventario != null)
            {
                if (inventario.Cantidad < cantidad && !inventario.Producto.IsService)
                {
                    Session["error"] = $"No tienes Stock suficiente para {cantidad} del producto {inventario.Producto.Name}.";
                    ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "FullName");
                    return RedirectToAction(nameof(Index));
                }
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