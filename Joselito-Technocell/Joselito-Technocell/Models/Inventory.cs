using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }

       public string Name { get; set; }

        public string description { get; set; }

        public string ubication { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }

    
      
    }

}