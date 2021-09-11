using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class DetalleRequisition
    {
        [Key]
        public int DetalleRequisitionId { get; set; }

        public int Cantidad { get; set; }

        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }
        
        public int Unidad { get; set; }

        public int productId { get; set; }
        public virtual Product Product { get; set; }
        
        public int RequisitionsId { get; set; }
        public virtual Requisitions Requisitions { get; set; }
    }
}