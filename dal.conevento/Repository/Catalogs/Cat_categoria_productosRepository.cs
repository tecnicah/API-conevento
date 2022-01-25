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
using Microsoft.Extensions.Configuration;


namespace dal.conevento.Repository
{
    public class Cat_categoria_productosRepository : GenericRepository<CatCategoriaProducto>, ICat_categoria_productosRepository
    {
        public Cat_categoria_productosRepository(Db_ConeventoContext context) : base(context)
        {
            
        }
    }
}
