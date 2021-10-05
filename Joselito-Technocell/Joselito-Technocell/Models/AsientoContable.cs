using Joselito_Technocell.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class AsientoContable
    {
        [Key]
        public int IdAsientoContable { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset Fecha { get; set; }
        public string Glosa { get; set; }
        public string RazonSocial { get; set; }
        public TipoAsientoContable Tipo { get; set; }
        public virtual ICollection<DetalleAsiento> DetalleAsientos { get; set; }
    }
}