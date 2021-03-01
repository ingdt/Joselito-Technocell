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
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [DataType(dataType: DataType.Currency,ErrorMessage = "The file{0} it's not a prise")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Display(Name = "Price Bay")]
        public decimal PriceBay { get; set; }

        [Required(ErrorMessage = "The file{0} is required")]
        public double Stock { get; set; }

        public override int GetHashCode()
        {
            return InventoryId;
        }
    }

}