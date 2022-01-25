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

        [Display(Name = "Metodo de pago")]
        public int? IdMetodoPago { get; set; }

        public DateTime Fecha { get; set; }
        public EstadoFactura Estado { get; set; }
        public decimal Efectivo { get; set; }

        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("IdMetodoPago")]
        public virtual MetodoPago MetodoPago { get; set; }

        [ForeignKey("IdCaja")]
        public virtual Caja Caja { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

        public decimal ITBIS
        {
            get
            {

                decimal t = 0;

                foreach (var item in this.DetalleFacturas)
                {
                    t += (decimal)item.ITBIS;
                }

                return t;

            }
        }

        public decimal SubTotal { get {

                decimal t = 0;

                foreach (var item in this.DetalleFacturas)
                {
                    t += (decimal)item.SubTotal;
                }

                return t;

        }
        
        }

        public decimal Total
        {
            get
            {

                return SubTotal + ITBIS;

            }

        }
    }
}