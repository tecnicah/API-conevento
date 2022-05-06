using biz.conevento.Entities;
using biz.conevento.Repository.Coupons;
using dal.conevento.DBContext;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dal.conevento.Repository.Coupons
{
    public class CouponsRepository : GenericRepository<Cupone>, ICouponsRepository
    {
        public CouponsRepository(Db_ConeventoContext context) : base(context)
        {
            _context = context;
        }

        public ActionResult getCupons() {
            var cupons = _context.Cupones.Select(c => new
            {
              c.Id,
              c.Nombre,
              c.FechaInicial,
              c.FechaFinal,
              c.NumeroCupones,
              cuponesOcupados = c.Eventos.Where(x => x.IdCupon == c.Id).Count(),
              cuponesRestantes = c.NumeroCupones - c.Eventos.Where(x => x.IdCupon == c.Id).Count(),
              c.MontoPesos,
              c.MontoPorcentaje,
              c.Estatus,
              c.Type
            }).ToList();

            return new ObjectResult(cupons);
        }


        public string validaCupon(string cupon, int? idUser) {

            string mensaje = "";
            var _cupon = _context.Cupones.FirstOrDefault(x => x.Nombre.ToLower() == cupon.ToLower());
            if (_cupon != null) {
                if (DateTime.Today >= _cupon.FechaInicial && DateTime.Today <= _cupon.FechaFinal)
                {
                    if (_cupon.Estatus)
                    {
                        if (_context.Eventos.Where(x => x.IdCupon == _cupon.Id).Count() < _cupon.NumeroCupones)
                        {
                            if (idUser != null) {
                                var user = _context.Eventos.Where(x => x.IdUsuario == idUser).Select(c => c.IdCupon);
                                if (!user.Contains(_cupon.Id))
                                {
                                    mensaje = "Cupon valido";
                                }
                                else
                                {
                                    mensaje = "El cupon ya fue utilizado";
                                }
                            }
                            else
                            {
                                mensaje = "Cupon valido";
                            }
                        }
                        else {
                            mensaje = "El cupon se a agotado";
                        }
                    }
                    else {
                        mensaje = "El cupon no esta activo";
                    }
                }
                else {
                    mensaje = "El cupon a caducado";
                }
            }
            else
            {
                mensaje = "El cupon no existe";
            }
            

            return mensaje;
        }
    }
}
