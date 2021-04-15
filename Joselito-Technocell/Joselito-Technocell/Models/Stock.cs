using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Stock
    {
        public int StockId { get; set; }

        public virtual  ICollection<Product> Products { get; set; }

        public virtual ICollection<TipoUnidadeMedidas> TipodeUnidades { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}