using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Requisitions
    {
        public int RequesicionId { get; set; }

        public DateTime FechadePedido { get; set; }

        public DateTime FechadeEntrega { get; set; }

        public string Cantidad { get; set; }

        public int Unidad { get; set; } 

        public string Articulo { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public int DeparmentId { get; set; }

        public virtual Deparment Deparment { get; set; }

    }
}