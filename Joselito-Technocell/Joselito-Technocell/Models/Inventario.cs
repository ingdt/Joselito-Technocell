using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Inventario
    {
        [Key]
        public int InventarioId { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "Cantidad")]
        public decimal Cantidad { get; set; }
        public virtual Product Producto { get; set; }
    }
}