using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public string description { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

    }
}