using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Factura
    {
        [Key]
        public int FacturaId { get; set; }

        [Display(Name = "Cliente")]
        public int? IdCliente { get; set; }

        [Display(Name = "Caja")]
        public int IdCaja { get; set; }
        public DateTime Fecha { get; set; }
        public EstadoFactura Estado { get; set; }
        public decimal Efectivo { get; set; }

        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("IdCaja")]
        public virtual Caja Caja { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

        public decimal Total { get {

                decimal t = 0;

                foreach (var item in this.DetalleFacturas)
                {
                    t += (decimal)item.Total;
                }

                return t;

            } }
    }
}