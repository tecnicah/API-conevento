using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biz.conevento.Entities;
using biz.conevento.Repository;
using dal.conevento.DBContext;
using Microsoft.Extensions.Configuration;

namespace dal.conevento.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private IConfiguration _config;

        public UserRepository(Db_ConeventoContext context, IConfiguration config) : base(context)
        {
            _config = config;
        }
    }
}
