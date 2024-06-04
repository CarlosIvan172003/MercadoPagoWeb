using MercadoPagoWeb.Models;
using Microsoft.AspNetCore.Mvc;
using MercadoPago.Config;
using Microsoft.AspNetCore.Routing.Template;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using MercadoPago.Client.Payment;
using MercadoPago.Client.Common;
using Newtonsoft.Json;
using MercadoPagoWeb.Servicios;

namespace MercadoPagoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _Context;
        private readonly Peticiones _Peticiones;

        public HomeController(Peticiones peticiones, Context context)
        {
            _Peticiones = peticiones;
            _Context = context;
        }

        public IActionResult Index()
        {
            ViewBag.PaymentID = TempData["PaymentID"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearPreferencia([FromBody] Orden orden)
        {
            /*----------------- Access token de mercado pago -----------------*/
            MercadoPagoConfig.AccessToken = "TEST-124798443317507-060115-2216c7f64f18aa5b1592bb88c9927f83-426084685";

            /*- Creacion de la referencia -*/
            var Request = new PreferenceRequest
            {
                /*- Lista de productos -*/
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = orden.Titulo,
                        Quantity = orden.Cantidad,
                        CurrencyId = "MXN",
                        UnitPrice = orden.Precio,
                    }
                },
                /*- Redirecciones -*/
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "https://hostingpruebas.bsite.net/Home/Success",
                    Failure = "https://hostingpruebas.bsite.net/Home/Failure",
                    Pending = "https://hostingpruebas.bsite.net/Home/Pending"
                },
                AutoReturn = "approved",
                /*- Exclusion de motodos de pago -*/
                PaymentMethods = new PreferencePaymentMethodsRequest
                {
                    //ExcludedPaymentMethods = new List<PreferencePaymentMethodRequest>
                    //{
                    //    new PreferencePaymentMethodRequest { Id = "ticket" }  // Ejemplo de exclusión de un método de pago específico
                    //},
                    ExcludedPaymentTypes = new List<PreferencePaymentTypeRequest>
                    {
                        new PreferencePaymentTypeRequest { Id = "ticket" },
                        new PreferencePaymentTypeRequest { Id = "bank_transfer" }
                    }
                }
            };
            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(Request);

            return Json(preference);
        }

        public async Task<IActionResult> Success(string preference_id)
        {
            var DatosCompraString = await _Peticiones.GetPreferenceID(preference_id);
            var DatosCompra = JsonConvert.DeserializeObject<PreferenceRequest>(DatosCompraString);


            TempData["PaymentID"] = DatosCompraString;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Failure(string preference_id)
        {
            var DatosCompraString = await _Peticiones.GetPreferenceID(preference_id);
            var DatosCompra = JsonConvert.DeserializeObject<PreferenceRequest>(DatosCompraString);


            TempData["PaymentID"] = DatosCompraString;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Pending(string preference_id)
        {
            var DatosCompraString = await _Peticiones.GetPreferenceID(preference_id);
            var DatosCompra = JsonConvert.DeserializeObject<PreferenceRequest>(DatosCompraString);


            TempData["PaymentID"] = DatosCompraString;
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Notificacion([FromBody] WebhookNotification notification)
        {
            DatosPago datosPago = new DatosPago()
            {
                id = notification.Id,
                Accion = notification.Action,
                api_version = notification.ApiVersion,
                date_created = notification.DateCreated,
                live_mode = notification.LiveMode,
                type = notification.Type,
                user_id = notification.UserId,
            };

            _Context.Add(datosPago);

            await _Context.SaveChangesAsync();
            return Ok();
        }

        public class WebhookNotification
        {
            public string Id { get; set; }
            public string Action { get; set; }
            public string ApiVersion { get; set; }
            public string DateCreated { get; set; }
            public bool LiveMode { get; set; }
            public string Type { get; set; }
            public int UserId { get; set; }
        }


    }
}




/*----------------- Datos de la url -----------------*/

//payment_id = ID (identificador) del pago de Mercado Pago.
//status = status del pago. Por ejemplo: approved para un pago aprobado o pending para un pago pendiente.
//external_reference = Monto enviado al crear la preferencia de pago.
//merchant_order_id = ID (identificador) de la orden de pago generada en Mercado Pago.
//preference_id = Id de la referencia de pago, sirve para recuperar la informacion del pago realizado.


