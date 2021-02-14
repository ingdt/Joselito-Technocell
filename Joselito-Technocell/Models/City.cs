using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        public string Name { get; set; }

        public int DepartmentId { get; set; }

    //    [ManyToOne]
       // public Department Department { get; set; }

       // [OneToMany(CascadeOperations = CascadeOperation.All)]
       // public List<Customer> Customers { get; set; }

        public override int GetHashCode()
        {
            return CityId;
        }
    }

}