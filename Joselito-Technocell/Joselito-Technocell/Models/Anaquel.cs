using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Joselito_Technocell.Models
{
    public class Anaquel
    {
        [Key]
        public int AnaquelId { get; set; }
        public string Description { get; set; }

        public int InventotyId { get; set; }
        public virtual Inventory Inventory { get; set; }

        public virtual ICollection<RegistroAlmacen> RegistroAlmacens { get; set; }
    }
}