using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class DetalleRequisition
    {
        [key]
        public int DetalleRequisitionId { get; set; }

        public string Cantidad { get; set; }
        
        public int Unidad { get; set; }

        public int productId { get; set; }
        public virtual Product Product { get; set; }
        
        public int RequisitionsId { get; set; }
        public virtual Requisitions Requisitions { get; set; }
    }
}