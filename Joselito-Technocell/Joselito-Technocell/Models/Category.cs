using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(256, ErrorMessage ="The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 2)]
        [Index("Category_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(256, ErrorMessage = "El campo {0} puede contener un maximo {1} y un minimo {2} de caracteres", MinimumLength = 3)]
        public string Description { get; set; }

        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        public virtual ICollection<Product> Products { get; set; }
    }

}