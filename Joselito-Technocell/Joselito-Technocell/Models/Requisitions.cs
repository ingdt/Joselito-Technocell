using System;
using System.Collections.Generic;

namespace Joselito_Technocell.Models
{
    public class Requisitions
    {
        [key]
        public int RequisitionsId { get; set; }

        public DateTime FechadePedido { get; set; }

        public DateTime FechadeEntrega { get; set; }

        public string Cantidad { get; set; }

        public int Unidad { get; set; }

        public string Articulo { get; set; }


        public int DeparmentId { get; set; }

        public virtual Deparment Deparment { get; set; }
       // public virtual ICollection<Product> Products { get; set; }

    }
}