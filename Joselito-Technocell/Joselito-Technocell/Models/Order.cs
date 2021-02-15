using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int CompanyId { get; set; }

        public int CustomerId { get; set; }

        public int StateId { get; set; }

        public string Date { get; set; }

        public string Remarks { get; set; }

       // [ManyToOne]
        public Customer Customer { get; set; }

        public override int GetHashCode()
        {
            return OrderId;
        }
    }

}