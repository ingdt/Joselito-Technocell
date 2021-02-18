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

        [Display(Name = "Name")]
        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Logo { get; set; }

        public int DepartmentId { get; set; }

        public int CityId { get; set; }

       // [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<User> Users { get; set; }

       // [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Product> Products { get; set; }

       // [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<CompanyCustomer> CompanyCustomers { get; set; }

        public override int GetHashCode()
        {
            return CompanyId;
        }
    }

}