using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class OrdenDetails
    {
        [Key]
        public int OrdenDetailsId { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string Comentary { get; set; }

        public DateTime Registro { get; set; }

        public int Unidad { get; set; }
    }
}