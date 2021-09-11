using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public DateTime FechaReguistro { get; set; }
        public string Address { get; set; }
    }
}