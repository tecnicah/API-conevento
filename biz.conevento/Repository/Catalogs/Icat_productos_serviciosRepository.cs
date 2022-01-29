using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biz.conevento.Entities;
using Microsoft.AspNetCore.Mvc;
using biz.conevento.Models.Events;

namespace biz.conevento.Repository
{
    public interface Icat_productos_serviciosRepository : IGenericRepository<CatProductosServicio>
    {

        ICollection productos_by_id_navigate(int id);
        ICollection productos_by_cateid_navigate(int id_cat);

        ICollection productos_by_cateid_date(int id_cat, DateTime fehca_infecha_inicio);

        List<dtodispo> Productos_by_id_date(dtolista _dtolista);
    }
}
