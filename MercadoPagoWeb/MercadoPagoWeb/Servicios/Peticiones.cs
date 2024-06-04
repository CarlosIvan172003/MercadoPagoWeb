namespace MercadoPagoWeb.Servicios
{
    public class Peticiones
    {
        private readonly HttpClient _httpClient;
        private readonly string _AccessToken = "TEST-124798443317507-060115-2216c7f64f18aa5b1592bb88c9927f83-426084685";
        private readonly string url = "https://api.mercadopago.com/checkout/preferences";

        public Peticiones(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetPreferenceID(string Preferencia)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _AccessToken);

            var response = await _httpClient.GetAsync(url + "/" + Preferencia);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
