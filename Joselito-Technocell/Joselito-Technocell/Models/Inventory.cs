using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        [Required(ErrorMessage = "The file{0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        public int ProductId { get; set; }

        //[ManyToOne]
        public Product Product { get; set; }

        public int WarehouseId { get; set; }

        public string WarehouseName { get; set; }

        public double Stock { get; set; }

        public override int GetHashCode()
        {
            return InventoryId;
        }
    }

}