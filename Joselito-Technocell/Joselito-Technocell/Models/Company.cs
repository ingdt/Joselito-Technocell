﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Company
    {
        [key]
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public DateTime FechaReguistro { get; set; }
        public int AddressId { get;  set; }
        public Address Address { get; set; }
        //relation//
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Deparment> Deparments { get; set; }
    }
}