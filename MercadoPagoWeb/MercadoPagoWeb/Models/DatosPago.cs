using System.ComponentModel.DataAnnotations;

namespace MercadoPagoWeb.Models
{
    public class DatosPago
    {
        public string id { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
        public string api_version { get; set; } = string.Empty;
        public string date_created { get; set; } = string.Empty;
        public bool live_mode { get; set; }
        public string type { get; set; } = string.Empty;
        public int user_id { get; set; }

    }
}
