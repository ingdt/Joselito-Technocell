using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Joselito_TechnocellDbContext : DbContext
    {
        public Joselito_TechnocellDbContext() : base("DefaultConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<TipoUnidadeMedidas> TipoUnidadesategories { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Anaquel> Anaquels { get; set; }
        public DbSet<RegistroAlmacen> registroAlmacens { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Deparment> Deparments { get; set; }
        public DbSet<Requisitions> Requisitions { get; set; }
        public DbSet<User> Users { get; set; }

        public System.Data.Entity.DbSet<Joselito_Technocell.Models.DetalleRequisition> DetalleRequisitions { get; set; }
        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<Tax> Taxes { get; set; }
        // public System.Data.Entity.DbSet<Joselito_Technocell.Models.Sale> Sales { get; set; }
        // public DbSet<Supplier> Suppliers { get; set; }
        // public DbSet<OrdenDetails> OrdenDetails { get; set; }
    }
}