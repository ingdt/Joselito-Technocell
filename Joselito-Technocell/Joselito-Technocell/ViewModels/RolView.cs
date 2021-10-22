using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.ViewModels
{
    public class RolView
    {
        [Display(Name = "Role")]
        public string RolID { get; set; }
        public string Nombre { get; set; }
    }
}