using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class TipoUnidadeMedidas
    {
        [key]
        public int TipoUnidadeMedidasId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Simbol { get; set; }

        public virtual ICollection<RegistroAlmacen> RegistroAlmacens { get; set; }
    }

}