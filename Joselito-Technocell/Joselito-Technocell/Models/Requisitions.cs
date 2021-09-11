using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Joselito_Technocell.Models
{
    public class Requisitions
    {
        [Key]
        public int RequisitionsId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de pedido")]
        public DateTime FechadePedido { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de entrega")]
        public DateTime? FechadeEntrega { get; set; }

        public EstadoRequisicion Estado { get; set; }

        [Display(Name = "Suplidor")]
        public int SuplidorId { get; set; }

        public virtual Suplidor Suplidor { get; set; }

        public virtual ICollection< DetalleRequisition> Detalle { get; set; }
    }
}