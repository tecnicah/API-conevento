using biz.conevento.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace biz.conevento.Repository.Coupons
{
    public interface ICouponsRepository : IGenericRepository<Cupone>
    {
        string validaCupon(string cupon, int? idUser);
        ActionResult getCupons();
    }
}
