using System.Collections.Generic;

namespace Joselito_Technocell.Models
{
    public class Inventory
    {
        [key]
        public int InventoryId { get; set; }

        public string Description { get; set; }

        public string Latitud { get; set; }

        public virtual ICollection<Anaquel> Anaqueles { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Companys { get; set; }



    }

}