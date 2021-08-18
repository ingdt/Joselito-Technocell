using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Factura
    {
        [Key]
        public int FacturaId { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
    }
}