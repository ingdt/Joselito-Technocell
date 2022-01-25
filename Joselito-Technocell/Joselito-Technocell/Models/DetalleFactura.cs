using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Joselito_Technocell.Models
{
    public class DetalleFactura
    {
        [Key]
        public int DetalleFacturaId { get; set; }
        public int ProductId { get; set; }
        public int FacturaId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("FacturaId")]
        public virtual Factura Factura { get; set; }
        public decimal ITBIS
        {
            get
            {
                if (Product.ConImpuesto)
                {
                    var t = Precio * 0.18m;
                    return t * Cantidad;
                }
                return 0;
            }
        }

        public decimal SubTotal
        {
            get
            {
                return Precio * Cantidad;
            }
        }

        public decimal Total
        {
            get
            {
                return SubTotal + ITBIS;
            }
        }

    }
}