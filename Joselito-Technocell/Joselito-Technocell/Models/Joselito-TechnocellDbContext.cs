using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Models
{
    public class Joselito_TechnocellDbContext: DbContext
    {
        public Joselito_TechnocellDbContext() : base("DefaultConnection")
        {

        }
    }
}