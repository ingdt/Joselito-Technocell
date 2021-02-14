using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        //public List<City> Cities { get; set; }

        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        //public List<Customer> Customers { get; set; }

        public override int GetHashCode()
        {
            return DepartmentId;
        }
    }

}