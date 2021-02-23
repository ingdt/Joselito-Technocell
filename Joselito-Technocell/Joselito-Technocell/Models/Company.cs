using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        [Index("CompanyNameIndex", IsUnique = true)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        public string Address { get; set; }

        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        [Required(ErrorMessage = "The file{0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "The file{0} is required")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        public int CityId { get; set; }

        [NotMapped]
        public HttpPostedFileBase LogoFile { get; set; }

        public virtual City City { get; set; }
        
        public virtual Department Department { get; set; }
        
        // [OneToMany(CascadeOperations = CascadeOperation.All)]
        public virtual List<User> Users { get; set; }

       // [OneToMany(CascadeOperations = CascadeOperation.All)]
        public virtual List<Product> Products { get; set; }

       // [OneToMany(CascadeOperations = CascadeOperation.All)]
        public virtual List<CompanyCustomer> CompanyCustomers { get; set; }

        public override int GetHashCode()
        {
            return CompanyId;
        }
    }

}