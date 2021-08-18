namespace Joselito_Technocell.Models
{
    public class DetalleFactura
    {
        public int DetalleFacturaId { get; set; }
        public int ProductId { get; set; }
        public int FacturaId { get; set; }
        public int Unidad { get; set; }
        public double PrecioUnidad { get; set; }
        public double SubTotal { get; set; }
    }
}