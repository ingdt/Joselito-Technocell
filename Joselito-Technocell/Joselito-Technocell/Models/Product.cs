using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(256, ErrorMessage = "El campo {0} puede contener un maximo {1} y un minimo {2} de caracteres", MinimumLength = 3)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(256, ErrorMessage = "El campo {0} puede contener un maximo {1} y un minimo {2} de caracteres", MinimumLength = 3)]
        public string Description { get; set; }

        [Display(Name = "Cod. de barras")]
        //[Index("ProductBarCodeIndex", IsUnique = true)]
        public string BarCode { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Display(Name = "Precio")]
        public decimal Price { get; set; }

        [Display(Name = "imagen")]
        public string Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Servicio?")]
        public bool IsService { get; set; }

        [Display(Name = "Categoria")]

        public int CategoryId { get; set; }

        // [ManyToOne]
        public virtual Category Category { get; set; }
        public virtual ICollection<DetalleRequisition> Detalles { get; set; }

        public virtual ICollection<RegistroAlmacen> RegistroAlmacens { get; set; }

        public string ImageFullPath { get { return string.Format("http://zulu-software.com/ECommerce{0}", Image.Substring(1)); } }

        public override int GetHashCode()
        {
            return ProductId;
        }
    }

}