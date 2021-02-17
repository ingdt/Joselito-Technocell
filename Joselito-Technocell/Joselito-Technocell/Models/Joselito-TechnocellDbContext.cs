using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Joselito_TechnocellDbContext: DbContext
    {
        public Joselito_TechnocellDbContext() : base("DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<Joselito_Technocell.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Joselito_Technocell.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<Joselito_Technocell.Models.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<Joselito_Technocell.Models.Company> Companies { get; set; }

        public System.Data.Entity.DbSet<Joselito_Technocell.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<Joselito_Technocell.Models.Inventory> Inventories { get; set; }

        public System.Data.Entity.DbSet<Joselito_Technocell.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<Joselito_Technocell.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<Joselito_Technocell.Models.Tax> Taxes { get; set; }

        public System.Data.Entity.DbSet<Joselito_Technocell.Models.Sale> Sales { get; set; }

        public System.Data.Entity.DbSet<Joselito_Technocell.Models.User> Users { get; set; }
    }
}