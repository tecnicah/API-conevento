using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biz.conevento.Entities;
using Microsoft.AspNetCore.Mvc;

namespace biz.conevento.Repository
{
    public interface IVariables_sistemaRepository : IGenericRepository<VariablesSistema>
    {
        string get_params(string variable);
    }
}
