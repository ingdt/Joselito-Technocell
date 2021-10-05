using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Cuenta
    {
        [Key]
        public int IdCuenta { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<DetalleAsiento> DetalleAsientos { get; set; }
    }
}