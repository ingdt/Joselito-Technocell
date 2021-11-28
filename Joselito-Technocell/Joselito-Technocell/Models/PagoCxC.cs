using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class PagoCxC
    {
        [Key]
        public int IdPagoCxC { get; set; }
        public int IdCxC { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public virtual CxC CxC { get; set; }
    }
}