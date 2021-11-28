using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Ingresos
    {
        [Key]
        public int IdIngreso { get; set; }

        public DateTime? Fecha { get; set; }

        public int IdCaja { get; set; }

        public DateTime? FechaEmision { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        [DataType(DataType.Currency)]
        public decimal? TotalIngreso { get; set; }

        public string Observacion { get; set; }

        [ForeignKey("IdCaja")]
        public virtual Caja Caja { get; set; }
    }
}