using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biz.conevento.Entities;
using biz.conevento.Models.Email;
using biz.conevento.Repository;
using biz.conevento.Servicies;
using dal.conevento.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace dal.conevento.Repository
{
    public class EventosRepository : GenericRepository<Evento>, IEventosRepository
    {
      
        

        public EventosRepository(Db_ConeventoContext context) : base(context)
        {
            _context = context;
        }

        public ActionResult GetServicesDEtailByUserId(int id)
        {
            var resultado = _context.Eventos.Select(x => new
            {
                x.NombreContratane,
                x.FechaHoraInicio,
                x.Correo,
                x.CalleNumero,
                x.Colonia,
                x.Cp,
                x.Id,
                x.IdUsuario,
                x.Telefono,
                x.IdCatMunicipioNavigation.Municipio,
                total_sum = String.Format("{0:0.00#}", x.ListaProductosEventos.Select(c => c.IdCatProductoNavigation.PrecioPorUnidad * c.CantidadHoras * c.CantidadUnidades).Sum()),
                total = String.Format("{0:0.00#}", x.Total),
                ListaProductosEventos = x.ListaProductosEventos.Select(j => new {
                 j.IdCatProductoNavigation.PrecioPorUnidad,
                 j.IdCatProductoNavigation.Producto,
                 j.CantidadUnidades,
                 j.CantidadHoras,
                 j.IdCatProductoNavigation.EspecificarTiempo
                 
                })

            }).Where(x => x.IdUsuario == id).ToList();


            return new ObjectResult(resultado);

        }

        public ActionResult GetEventDtailById(int id)
        {
            var resultado = _context.Eventos.Select(x => new
            {
                x.NombreContratane,
                x.FechaHoraInicio,
                x.FechaHoraFin,
                x.GenteEsperada,
                x.Correo,
                x.CalleNumero,
                x.NombreEvento,
                x.Colonia,
                x.Cp,
                x.Id,
                x.IdUsuario,
                x.Telefono,
                x.IdCatMunicipioNavigation.Municipio,
                x.FormaPago,
                x.ReferenciaPago,
                ReqFactura = x.ReqFactura == true ? "SI": "NO",
                flete  = x.ListaProductosEventos.Where(o => (o.IdCatProductoNavigation.IdCategoriaProducto == 6) || (o.IdCatProductoNavigation.IdCategoriaProducto == 7)).Count(),
                total_servicios = x.ListaProductosEventos.Count(),
                total_sum = String.Format("{0:0.00#}", x.ListaProductosEventos.Select(c => c.IdCatProductoNavigation.PrecioPorUnidad * c.CantidadHoras * c.CantidadUnidades).Sum()),
                total = String.Format("{0:0.00#}", x.Total),
                ListaProductosEventos = x.ListaProductosEventos.Select(j => new {
                    j.IdCatProductoNavigation.PrecioPorUnidad,
                    j.IdCatProductoNavigation.Producto,
                    j.CantidadUnidades,
                    j.CantidadHoras,
                    j.IdCatProductoNavigation.EspecificarTiempo,
                    monto = j.IdCatProductoNavigation.PrecioPorUnidad * j.CantidadUnidades * j.CantidadHoras

                })

            }).First(x => x.Id == id);


            return new ObjectResult(resultado);

        }

        public PaymentIntentService stripe()
        {
            StripeConfiguration.ApiKey = "pk_test_51JcbT7L3Jg9RumxpabSHFoQp4IhGNIIva8ZnhASqYQyxnseDFXSrD23BUfrjupwbJbWfzp3NjDWliKOSPJWOGt0o00f03HF62U";

            var options = new PaymentIntentCreateOptions
            {
                Amount = 1099,
                Currency = "eur",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            };
            var service = new PaymentIntentService();
            service.Create(options);

            return service;
        }

        public string get_params(string variable)
        {
            var resultado = _context.VariablesSistemas.FirstOrDefault(v => v.Variable == variable).ValorVariable;
            return resultado;
        }

        public ActionResult GetServicesDetailByfilter(DateTime fechaInicial, DateTime fechaFinal, int id_municipio )
        {
            var resultado = _context.Eventos.Select(x => new
            {
                x.NombreContratane,
                x.FechaHoraInicio,
                x.Correo,
                x.CalleNumero,
                x.Colonia,
                x.Cp,
                x.Id,
                x.IdUsuario,
                x.Telefono,
                x.IdCatMunicipioNavigation.Municipio,
                x.IdCatMunicipio,
                x.FormaPago,
                x.ReferenciaPago,
                total_servicios = x.ListaProductosEventos.Count(),
                usuario = (x.IdUsuarioNavigation.Nombres == null ? "Sin" : x.IdUsuarioNavigation.Nombres) + " " + (x.IdUsuarioNavigation.Apellidos == null ? "Usuario" : x.IdUsuarioNavigation.Apellidos),
                total_sum = String.Format("{0:0.00#}", x.ListaProductosEventos.Select(c => c.IdCatProductoNavigation.PrecioPorUnidad * c.CantidadHoras * c.CantidadUnidades).Sum()),
                total = String.Format("{0:0.00#}", x.Total),               
                ListaProductosEventos = x.ListaProductosEventos.Select(j => new {
                    j.IdCatProductoNavigation.PrecioPorUnidad,
                    j.IdCatProductoNavigation.Producto,
                    j.CantidadUnidades,
                    j.CantidadHoras,
                    j.IdCatProductoNavigation.EspecificarTiempo

                })

            }).Where(x => x.FechaHoraInicio >= (fechaInicial == null ? x.FechaHoraInicio : fechaInicial) 
            && x.FechaHoraInicio <= (fechaInicial == null ? x.FechaHoraInicio : fechaFinal)
            && x.IdCatMunicipio == (id_municipio == 0 ? x.IdCatMunicipio : id_municipio)).ToList();

            return new ObjectResult(resultado);

        }

    }
}
