using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public DateTime FechaReguistro { get; set; }
        //relation//
        public int AddressID { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }

    }
}