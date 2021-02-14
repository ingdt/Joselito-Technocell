using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class CompanyCustomer
    {
        [Key]
        public int CompanyCustomerId { get; set; }

        public int CompanyId { get; set; }

        public int CustomerId { get; set; }

        //[ManyToOne]
        //public Company Company { get; set; }

        //[ManyToOne]
        //public Customer Customer { get; set; }

        public override int GetHashCode()
        {
            return CompanyCustomerId;
        }
    }

}