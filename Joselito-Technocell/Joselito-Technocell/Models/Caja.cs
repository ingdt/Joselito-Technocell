using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Caja
    {
        [Key]
        public int CajaId { get; set; }
        public DateTime Apertura { get; set; }
        public DateTime Cierre { get; set; }
        public EstadoCaja Estdo { get; set; }
        public double MontoApertura { get; set; }
        public double MontoCierre { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }

    }
}