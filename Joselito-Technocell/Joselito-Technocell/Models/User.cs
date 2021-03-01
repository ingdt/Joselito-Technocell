using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        [Index("UserEmailIndex", IsUnique = true)]
        [DataType (DataType.EmailAddress)]
        public string UserName { get; set; }

        [Display(Name = "Firs Name")]
        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        public string LastName { get; set; }

        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        public string Address { get; set; }

        [NotMapped]
        public HttpPostedFileBase PhotoFile { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsUser { get; set; }

        public bool IsCustomer { get; set; }

        public bool IsSupplier { get; set; }

        public bool IsRemembered { get; set; }

        public string Password { get; set; }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        public string PhotoFullPath { get;}

        public override int GetHashCode()
        {
            return UserId;
        }
    }

}