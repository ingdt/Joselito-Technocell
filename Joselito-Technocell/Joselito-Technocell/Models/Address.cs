using System.Collections.Generic;

namespace Joselito_Technocell.Models
{
    public class Address
    {
        [key]
        public int AddressId { get; set; }
        public string StringAddress { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}