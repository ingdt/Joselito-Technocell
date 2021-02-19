using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Joselito_Technocell.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Display(Name = "Department")]
        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        [Index("DepartmentNameIndex", IsUnique = true)]
        public string Name { get; set; }

       // [OneToMany(CascadeOperations = CascadeOperation.All)]
        public virtual List<City> Cities { get; set; }

       // [OneToMany(CascadeOperations = CascadeOperation.All)]
        public virtual List<Customer> Customers { get; set; }

        public override int GetHashCode()
        {
            return DepartmentId;
        }
    }

}