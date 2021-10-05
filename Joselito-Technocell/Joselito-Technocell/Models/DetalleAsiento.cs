using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class DetalleAsiento
    {
        [Key]
        public int IdDetalleAsiento { get; set; }
        public int IdAsientoContable { get; set; }
        public int CuentaIdCuenta { get; set; }
        public string Detalle { get; set; }
        public decimal Debe { get; set; }
        public decimal Hacer { get; set; }

        public virtual AsientoContable Asiento { get; set; }
        public virtual Cuenta Cuenta { get; set; }
    }
}