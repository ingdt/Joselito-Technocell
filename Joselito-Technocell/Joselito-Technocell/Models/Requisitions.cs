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

        public int DeparmentId { get; set; }

        public virtual ICollection< DetalleRequisition> Detalle { get; set; }
    }
}