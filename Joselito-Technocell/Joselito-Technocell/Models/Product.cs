﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(256, ErrorMessage = "The field {0} can contain maximun {1} and minimum {2} characters", MinimumLength = 3)]
        public string Description { get; set; }

        [Display(Name = "Bar Code")]
        [Index("ProductBarCodeIndex", IsUnique = true)]
        public string BarCode { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        public string Image { get; set; }


        public HttpPostedFileBase ImageFile { get; set; }

        public double? Stock { get; set; }

        [Display(Name = "Is Service")]
        public bool IsService { get; set; }


        [Display(Name = "Supplier")]
        public int? SupplierId { get; set; }

        public Supplier supplier { get; set; }

        public virtual IEnumerable<OrdenDetails>  OrdenDetails { get; set; }

        public virtual IEnumerable<Category>  Categories { get; set; }


        public int CategoryId { get; set; }

       // [ManyToOne]
        public Category Category { get; set; }


        public string ImageFullPath { get { return string.Format("http://zulu-software.com/ECommerce{0}", Image.Substring(1)); } }

        public override int GetHashCode()
        {
            return ProductId;
        }
    }

}