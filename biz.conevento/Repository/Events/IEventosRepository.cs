using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biz.conevento.Entities;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace biz.conevento.Repository
{
    public interface IEventosRepository : IGenericRepository<Evento>
    {

        ActionResult GetServicesDEtailByUserId(int id);
        ActionResult GetServicesDetailByfilter(DateTime fechaInicial, DateTime fechaFinal, int id_municipio);
        ActionResult GetEventDtailById(int id);
        PaymentIntentService stripe();
        string get_params(string variable);
    }
}
