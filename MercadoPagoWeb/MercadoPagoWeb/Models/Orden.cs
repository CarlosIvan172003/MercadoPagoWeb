namespace MercadoPagoWeb.Models
{
    public class Orden
    {
        public string Titulo { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
