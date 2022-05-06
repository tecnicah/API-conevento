using api.conevento.Models;
using biz.conevento.Entities;
using biz.conevento.Repository.Coupons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.conevento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuponController : Controller
    {
        private readonly ICouponsRepository _couponsRepository;

        public CuponController(
            ICouponsRepository couponsRepository
           )
        {
            _couponsRepository = couponsRepository;
        }

        [HttpPost("AddCoupon", Name = "AddCoupon")]
        public async Task<ActionResult<ApiResponse<Evento>>> AddCoupon(Cupone _cupone)
        {
            var response = new ApiResponse<Cupone>();

            try
            {
                var exit = _couponsRepository.FindBy(x => x.Nombre.ToLower() == _cupone.Nombre.ToLower());

                if(exit.Any())
                {
                    response.Success = true;
                    response.Message = "Ya existe un cupon con ese nombre";
                    
                }
                else
                {
                    var Sevento = _couponsRepository.Add(_cupone);
                    response.Result = Sevento;
                    response.Success = true;
                    response.Message = "Cupon agregado correctamente";
                }
               
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPut("EditCoupon", Name = "EditCoupon")]
        public async Task<ActionResult<ApiResponse<Cupone>>> EditCoupon(Cupone _cupone)
        {
            var response = new ApiResponse<Cupone>();

            try
            {
                var Sevento = _couponsRepository.Update(_cupone, _cupone.Id);
                response.Result = Sevento;
                response.Success = true;
                response.Message = "Cupon editado correctamente";
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = ex.ToString();
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpGet("GetCoupons", Name = "GetCoupons")]
        public async Task<ActionResult<ApiResponse<Cupone>>> GetCoupons()
        {
            var response = new ApiResponse<Cupone>();

            try
            {
                var Sevento = _couponsRepository.getCupons();
                return StatusCode(202, new
                {
                    Success = true,
                    Result = Sevento,
                    Message = ""
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Result = 0, Message = $"Internal server error {ex.Message}" });
            }
        }

        [HttpGet("GetCouponById", Name = "GetCouponById")]
        public ActionResult GetCouponById(int id)
        {
            try
            {
                var Sevento = _couponsRepository.FindBy(x => x.Id == id).ToList();
                return StatusCode(202, new
                {
                    Success = true,
                    Result = Sevento,
                    Message = ""
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Result = 0, Message = $"Internal server error {ex.Message}" });
            }
        }

        [HttpGet("ValidateCoupon", Name = "ValidateCoupon")]
        public ActionResult ValidateCoupon(string cupon, int? userId)
        {
            try
            {
                var Sevento = _couponsRepository.validaCupon(cupon, userId);
                return StatusCode(202, new
                {
                    Success = true,
                    Result = _couponsRepository.FindBy(x => x.Nombre.ToLower() == cupon.ToLower()).ToList(),
                    Message = Sevento
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Result = 0, Message = $"Internal server error {ex.Message}" });
            }
        }
    }
}
