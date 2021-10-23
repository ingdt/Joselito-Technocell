using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class CxC
    {
        [Key]
        public int IdCxC { get; set; }
        public int? IdCliente { get; set; }
        public int? FacturaId { get; set; }
        public string Descripcion { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal Monto { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal Resto { get; set; }
        public bool Saldado { get; set; }
        public virtual Cliente Cliente { get; set; }

        public virtual Factura Factura { get; set; }
    }
}