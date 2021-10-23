using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Joselito_TechnocellDbContext : IdentityDbContext<User>
    {
        public Joselito_TechnocellDbContext() : base("DefaultConnection")
        {
        }

        public static Joselito_TechnocellDbContext Create()
        {
            return new Joselito_TechnocellDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.UserId, r.RoleId }).ToTable("UserRole");
            modelBuilder.Entity<IdentityUserLogin>().HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId }).ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<RegistroAlmacen> registroAlmacens { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Requisitions> Requisitions { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<DetalleRequisition> DetalleRequisitions { get; set; }
        public DbSet<Suplidor> Suplidors { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Caja> Cajas { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<MetodoPago> MetodoPagos { get; set; }
        public DbSet<DetalleFactura> DetalleFacturas { get; set; }
        public DbSet<AsientoContable> AsientoContables { get; set; }
        public DbSet<Cuenta> CuentasContables { get; set; }
        public DbSet<DetalleAsiento> DetalleAsientosContables { get; set; }
        public DbSet<CxC> CxC { get; set; }
        public DbSet<Ingresos> Ingresos { get; set; }
        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<Tax> Taxes { get; set; }
        // public System.Data.Entity.DbSet<Joselito_Technocell.Models.Sale> Sales { get; set; }
        // public DbSet<Supplier> Suppliers { get; set; }
        // public DbSet<OrdenDetails> OrdenDetails { get; set; }
    }
}