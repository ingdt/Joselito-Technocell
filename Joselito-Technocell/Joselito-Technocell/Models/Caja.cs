using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Caja
    {
        [Key]
        public int CajaId { get; set; }
        public DateTime Apertura { get; set; }
        public DateTime? Cierre { get; set; }
        public EstadoCaja Estdo { get; set; }
        public double MontoApertura { get; set; }
        public double MontoCierre { get; set; }
        public string OperadorId { get; set; }

        [ForeignKey("OperadorId")]
        public virtual User Operador { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<Ingresos> Ingresos { get; set; }

    }
}