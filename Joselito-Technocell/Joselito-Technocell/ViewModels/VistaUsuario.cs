using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.ViewModels
{
    public class VistaUsuario
    {
        public string UsuarioID { get; set; }
        public string Nombre { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public RolView Role { get; set; }
        public List<RolView> Roles { get; set; }
    }
}