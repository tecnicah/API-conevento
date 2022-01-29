using biz.conevento.Entities;
using biz.conevento.Repository.Catalogs;
using dal.conevento.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace dal.conevento.Repository.Catalogs
{
    public class catSubcategoriaProductosRepository : GenericRepository<CatSubcategoriaProducto>, IcatSubcategoriaProductosRepository
    {
        public catSubcategoriaProductosRepository(Db_ConeventoContext context) : base(context)
        {
            _context = context;
        }
    }
}
