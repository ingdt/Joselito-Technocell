using Joselito_Technocell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joselito_Technocell.Helpers
{
    public class Seed
    {
        static Joselito_TechnocellDbContext db = new Joselito_TechnocellDbContext();

        public static void CreateMEtodosDePago()
        {
            if (!db.MetodoPagos.Any())
            {
                var m = new MetodoPago
                {
                    Descripcion = "Al contado"
                };

                db.MetodoPagos.Add(m);
                db.SaveChanges();

                m = new MetodoPago
                {
                    Descripcion = "Credito"
                };

                db.MetodoPagos.Add(m);
                db.SaveChanges();
            }
        }
    }
}