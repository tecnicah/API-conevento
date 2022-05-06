using biz.conevento.Entities;
using biz.conevento.Repository.Catalogs;
using dal.conevento.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace dal.conevento.Repository.Catalogs
{
    public class MunicipiosRepository : GenericRepository<CatMunicipio>, IMunicipiosRepository
    {
        public MunicipiosRepository(Db_ConeventoContext context) : base(context)
        {

        }
    }
}
