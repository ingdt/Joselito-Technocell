using System;
namespace Joselito_Technocell.Models
{
    public class RegistroAlmacen
    {
        [key]
        public int RegistroAlmacenId { get; set; }
        public int ProductosId { get; set; }
        public string codigo { get; set; }
        public int AnaquelId { get; set; }
        public int Cantidad { get; set; }
        public bool EntradaSalida { get; set; }
        public int TipoUnidadMedida { get; set; }
        public DateTime FechaRegistro { get; set; }
        public virtual Product Product { get; set; }
        public virtual Anaquel Anaquel{ get; set; }
        public virtual TipoUnidadeMedidas TipoUnidadeMedidas { get; set; }
    }
}