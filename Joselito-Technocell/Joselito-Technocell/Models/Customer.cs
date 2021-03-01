using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Customer
    {
        [Key]//, AutoIncrement]
        public int CustomerId { get; set; }
    
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 1)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} characters")]
        public string LastName { get; set; }

        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        [DataType(DataType.PhoneNumber)] public string Phone { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

       // [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Order> Orders { get; set; }

      //  [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Sale> Sales { get; set; }

        public bool IsUpdated { get; set; }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        public string PhotoFullPath
        {
            get
            {
                return Photo == null ? string.Empty : string.Format("http://zulu-software.com/ECommerce{0}", Photo.Substring(1));
            }
        }

        public override int GetHashCode()
        {
            return CustomerId;
        }
    }

}