using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using api.conevento.ActionFilter;
using api.conevento.Models;
using biz.conevento.Entities;
using biz.conevento.Repository;
using biz.conevento.Servicies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using api.conevento.Models.Events;
using api.conevento.Models.User;
using System.Text;
using Stripe;

namespace api.conevento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IUserRepository _userRepository;
        private readonly IEventosRepository _eventoRepository;
       // private readonly IVariables_sistemaRepository _Variables_sistemaRepository;

        public EventosController(
             IMapper mapper,
             ILoggerManager logger,
             IUserRepository userRepository
            , IEventosRepository eventoRepository
            //, IVariables_sistemaRepository variablesRepository
            )
        {
            _mapper = mapper;
            _logger = logger;
            _userRepository = userRepository;
            _eventoRepository = eventoRepository;
        //    _Variables_sistemaRepository = variablesRepository;
        }

        [HttpPost("AddEvent", Name = "AddEvent")]
        public async Task<ActionResult<ApiResponse<Evento>>> AddEvent(EventoDto _evento)
        {
            var response = new ApiResponse<Evento>();

            try
            {
                 _evento.FechaCreacion = DateTime.Now;
                 _evento.FechaPago = DateTime.Now;
                var Sevento = _eventoRepository.Add(_mapper.Map<Evento>(_evento));
                StreamReader reader = new StreamReader(Path.GetFullPath("TemplateMail/Emaile.html"));
                string pathimg = _userRepository.GetConfiguration("path_imagenes");
                string body = string.Empty;
                body = reader.ReadToEnd();
                body = body.Replace("{user}", _evento.NombreContratane);
                body = body.Replace("{username}", $"{_evento.NombreContratane}");
                body = body.Replace("{path_imagenes}", pathimg);
                body = body.Replace("{pass}", _evento.FechaHoraInicio.ToString());

                _userRepository.SendMail(_evento.Correo, body, "Bienvenido a Conevento");

                response.Result = Sevento;
                response.Success = true;
                response.Message = "Evento creado con exíto";

            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = "Internal server error";
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPost("AddEventAdmin", Name = "AddEventAdmin")]
        public async Task<ActionResult<ApiResponse<Evento>>> AddEventAdmin(List<EventoDto> _evento)
        {
            var response = new ApiResponse<Evento>();

            try
            {
                //foreach (var element in _evento)
                //{
                //    element.FechaCreacion = DateTime.Now;
                //    element.FechaPago = DateTime.Now;
                //    var Sevento = _eventoRepository.Add(_mapper.Map<Evento>(element));
                //}

                //_evento.FechaCreacion = DateTime.Now;
                //_evento.FechaPago = DateTime.Now;
                for(int i = 0; i < _evento.Count(); i++)
                {
                    response.Result = _eventoRepository.Add(_mapper.Map<Evento>(_evento[i]));
                }

                response.Success = true;
                response.Message = "Evento creado con exíto";

            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = "Internal server error";
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPost("GetEventsbyuser", Name = "GetEventsbyuser")]
        public ActionResult GetEventsbyuser(int id_user)
        {
            try
            {
                IQueryable<Evento> _eventos = null;
                //_eventos = _eventoRepository.FindBy(x => x.IdUsuario == id_user).Include(i => i.ListaProductosEventos).ThenInclude(z =>z.IdCatProductoNavigation);
                var eventos = _eventoRepository.GetServicesDEtailByUserId(id_user);



                return StatusCode(202, new
                {
                    Success = true,
                    Result = eventos,
                    Message = ""
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, new { Success = false, Result = 0, Message = $"Internal server error {ex.Message}" });
            }
        }

        [HttpPost("GetEventDtailById", Name = "GetEventDtailById")]
        public ActionResult GetEventDtailById(int id_)
        {
            try
            {
                //IQueryable<Evento> _eventos = null;
                //_eventos = _eventoRepository.FindBy(x => x.IdUsuario == id_user).Include(i => i.ListaProductosEventos).ThenInclude(z =>z.IdCatProductoNavigation);
                var evento = _eventoRepository.GetEventDtailById(id_);



                return StatusCode(202, new
                {
                    Success = true,
                    Result = evento,
                    Message = ""
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, new { Success = false, Result = 0, Message = $"Internal server error {ex.Message}" });
            }
        }


        [HttpPost("GetServicesDetailByfilter", Name = "GetServicesDetailByfilter")]
        public ActionResult GetServicesDetailByfilter(data_model _data_model)
        {
            try
            {
                if (_data_model.fechaInicial.Year < 2005)
                    _data_model.fechaInicial = new DateTime(2005, 05, 25);
                if (_data_model.fechaFinal.Year < 2005)
                    _data_model.fechaFinal = new DateTime(2055, 05, 25);

                var eventos = _eventoRepository.GetServicesDetailByfilter(_data_model.fechaInicial, _data_model.fechaFinal, _data_model.id_municipio);

                return StatusCode(202, new
                {
                    Success = true,
                    Result = eventos,
                    Message = ""
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, new { Success = false, Result = 0, Message = $"Internal server error {ex.Message}" });
            }
        }

        /// <summary>
        /// ///////////////////////// STRIPE ///////////////////////////////////////////
        /// </summary>

        [HttpGet("secret_stripe", Name = "secret_stripe")]
        public ActionResult secret_stripe()
        {
           // var intent = _eventoRepository.stripe();  // ... Fetch or create the PaymentIntent
                                                      // return Json(new { client_secret = intent.ClientSecret });

            StripeConfiguration.ApiKey = "sk_test_51JcbT7L3Jg9Rumxp8dtMRXX4wtU57GRv3jyQoNerdJXI9L6q70GvL0PauxF8EQ7daQhxhTMk45EOVFaz4yzkY9qI00iT4p7Fdm";

            var options = new PaymentIntentCreateOptions
            {
                Amount = 1099,
                Currency = "mxn",
                PaymentMethodTypes = new List<string>
                  {
                    "oxxo",
                    "card"
                  },
            };
            var service = new PaymentIntentService();
            var intent =  service.Create(options);

            return StatusCode(202, new
            {
                Success = true,
                Result = new { client_secret = intent.ClientSecret },
                Message = ""
            });
        }

        [HttpPost("paymentintent_stripe_params", Name = "paymentintent_stripe_params")]
        public ActionResult paymentintent_stripe_params(SecretDto payment_intent_request)
        {
            try
            {
                var _ApiKey = _eventoRepository.get_params("StripeConfiguration.ApiKey");
                StripeConfiguration.ApiKey = _ApiKey;

                // StripeConfiguration.ApiKey = "sk_test_51JcbT7L3Jg9Rumxp8dtMRXX4wtU57GRv3jyQoNerdJXI9L6q70GvL0PauxF8EQ7daQhxhTMk45EOVFaz4yzkY9qI00iT4p7Fdm";

                var options = new PaymentIntentCreateOptions
                {
                    Amount = Convert.ToInt64(payment_intent_request.amount) , //esos son centavos
                    Currency = "mxn",
                    PaymentMethodTypes = new List<string>
                  {
                    "oxxo",
                    "card"
                  },
                    Metadata = new Dictionary<string, string>
                  {
                    { "integration_check", "accept_a_payment" },
                  },
                };
                var service = new PaymentIntentService();
                var intent = service.Create(options);

                return StatusCode(202, new
                {
                    Success = true,
                    Result = new { client_secret = intent.ClientSecret, options },
                    Message = ""
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, new { Success = false, Result = 0, Message = $"Internal server error {ex.Message}" });
            }
        }

        [HttpGet("CalendarEventByServicio", Name = "CalendarEventByServicio")]
        public ActionResult CalendarEventByServicio(int id)
        {
            try
            {
                var evento = _eventoRepository.CalendarEventByServicio(id);

                return StatusCode(202, new
                {
                    Success = true,
                    Result = evento,
                    Message = ""
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, new { Success = false, Result = 0, Message = $"Internal server error {ex.Message}" });
            }
        }

    }





    public class data_model
    {
        public data_model()
        {

        }

        public DateTime fechaInicial  { get; set; }
        public DateTime fechaFinal { get; set; }
        public int id_municipio { get; set; }

    }
}
