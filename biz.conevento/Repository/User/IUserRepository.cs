using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biz.conevento.Entities;

namespace biz.conevento.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        string SendMail(string emailTo, string body, string subject);
        string CreatePassword(int length);
    }
}
