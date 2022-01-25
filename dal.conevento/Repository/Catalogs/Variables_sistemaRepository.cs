using System;
using System.Collections;
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

namespace dal.conevento.Repository
{
   public  class Variables_sistemaRepository: GenericRepository<VariablesSistema>, IVariables_sistemaRepository
    {


        public Variables_sistemaRepository(Db_ConeventoContext context) : base(context)
        {
            _context = context;
        }


        public string get_params(string variable)
        {
            var resultado = _context.VariablesSistemas.FirstOrDefault(v => v.Variable == variable).ValorVariable;
            return resultado;
        }

    }
}
