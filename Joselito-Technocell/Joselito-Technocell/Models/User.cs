using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class User : IdentityUser
    {

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "el campo {0} es requerido")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "el campo {0} es requerido")]
        public string LastName { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Teléfono")]
        [RegularExpression("^[0-9]{3}[-][0-9]{3}[-][0-9]{4}$", ErrorMessage = "Por favor introduzca un Número con el formato correcto ejemplo: 000-000-0000")]
        public override string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Por favor introduzca una direccion de correo valida")]
        public override string Email { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }

        public override string ToString()
        {
            return FullName;
        }

        internal async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager userManager)
        {
             // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await userManager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

}