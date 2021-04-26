using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Note_of_Credit
    {
        public int NoteofCreditID { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get;  set; }

        public int NumerodeTelefono { get; set; }

        public int MontodelCredito { get; set; }
    }
}