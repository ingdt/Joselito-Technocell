using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(256, ErrorMessage ="The field {0} can contain maximun {1} and minimum {2} characters",MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Range(1, double.MaxValue, ErrorMessage = "The file{0} is reguired")]
        public int DepartmentId { get; set; }

        //[ManyToOne]
        public Department Department { get; set; }

        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        public virtual List<Customer> Customers { get; set; }

        public override int GetHashCode()
        {
            return CityId;
        }
    }

}