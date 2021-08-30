using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Suplidor
    {
        [Key]
        public int SuplidorId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre del proveedor")]
        public string SuplidorNombre { get; set; }

        [Display(Name = "Direccion del proveedor")]
        public string SuplidorDireccion1 { get; set; }

        [StringLength(50)]
        [DataType(DataType.Url)]
        [Display(Name = "Pagina web del proveedor")]
        public string Paginaweb { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(12)]
        public string Telefono1 { get; set; }

        [StringLength(12)]
        public string Telefono2 { get; set; }

        [StringLength(500)]
        public string Observacion { get; set; }

        [Column(TypeName = "text")]
        [Display(Name = "Direccion 2 del proveedor")]
        public string Direccion2 { get; set; }
    }
}