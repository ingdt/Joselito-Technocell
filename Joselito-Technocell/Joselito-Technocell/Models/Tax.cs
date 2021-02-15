using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Tax
    {
        [Key]
        public int TaxId { get; set; }

        public string Description { get; set; }

        public double Rate { get; set; }

        public int CompanyId { get; set; }

       // [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Product> Products { get; set; }

        public override int GetHashCode()
        {
            return TaxId;
        }
    }

}