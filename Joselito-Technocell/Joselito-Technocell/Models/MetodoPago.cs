using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class MetodoPago
    {
        [Key]
        public int IdMetodoPago { get; set; }

        [MaxLength(50, ErrorMessage = "el campo {0} no acepta mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Descripcion { get; set; }
    }
}