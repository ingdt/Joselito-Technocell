﻿
using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace Joselito_Technocell.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        public string Description { get; set; }

        public int CompanyId { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Product> Products { get; set; }

        public override int GetHashCode()
        {
            return CategoryId;
        }

    }

}