using Joselito_Technocell.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Helpers
{
    public class Helper : IDisposable
    {
        private static Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();
        #region getComboBox
        internal static IEnumerable GetDepartments()
        {
            var departments = db.Departments.ToList();
            departments.Add(new Department
            {
                DepartmentId = 0,
                Name = "[Select a departmant]"
            });
            return departments.OrderBy(d => d.Name).ToList();
        }

        internal static IEnumerable GetCities()
        {
            var Cities = db.Cities.ToList();
            Cities.Add(new City
            {
                CityId = 0,
                Name = "[Select a City ]"
            });
            return Cities.OrderBy(d => d.Name).ToList();
        }

        internal static IEnumerable GetProduct()
        {
            var Products = db.Products.ToList();
            Products.Add(new Product
            {
                ProductId = 0,
                BarCode = "[Select a Product ]"
            });
            return Products.OrderBy(d => d.BarCode).ToList();
        }

        internal static IEnumerable GetCompanies()
        {
            var Companies = db.Companies.ToList();
            Companies.Add(new Company
            {
                CompanyId = 0,
                
                Name= "[Select a Product ]"
            });
            return Companies.OrderBy(d => d.Name).ToList();
        }

        internal static IEnumerable GetTaxes()
        {
            var Taxes = db.Taxes.ToList();
            Taxes.Add(new Tax
            {
                TaxId = 0,
                Description = "[Select a Taxes ]"
            });
            return Taxes.OrderBy(d => d.Description).ToList();
        }

        internal static IEnumerable GetCategories()
        {
            var Categories = db.Categories.ToList();
            Categories.Add(new Category
            {
                CategoryId = 0,
                Name = "[Select a Categories ]"
            });
            return Categories.OrderBy(d => d.Name).ToList();
        }

        internal static IEnumerable GetCustomers()
        {
            var Customers = db.Customers.ToList();
            Customers.Add(new Customer
            {
                CustomerId = 0,
                FirstName = "[Select a Customer ]"
            });
            return Customers.OrderBy(d => d.FirstName).ToList();
        }
        #endregion


        public void Dispose()
        {
            db.Dispose();
        }
    }



}