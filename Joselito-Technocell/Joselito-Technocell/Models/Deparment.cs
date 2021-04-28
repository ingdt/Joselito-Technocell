using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Deparment
    {
        [key]
        public int DeparmentId { get; set; }
        public string Descripcion { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Requisitions> Requisitions { get; set; }
    }
}