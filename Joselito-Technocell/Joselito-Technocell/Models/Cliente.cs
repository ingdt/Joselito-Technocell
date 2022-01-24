using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        [RegularExpression("^[0-9]{3}[-][0-9]{3}[-][0-9]{4}$", ErrorMessage = "Por favor introduzca un Número con el formato correcto ejemplo: 000-000-0000")]
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public virtual ICollection<CxC> CxC { get; set; }

        public string FullName { get { return $"{this.Nombre} {this.Apellido}"; } }
    }
}