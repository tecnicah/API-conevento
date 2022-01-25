using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using api.conevento.ActionFilter;
using api.conevento.Models;
using biz.conevento.Entities;
using biz.conevento.Repository;
using biz.conevento.Servicies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using api.conevento.Models.User;
using System.Text;
using System.Collections;

namespace api.conevento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IUserRepository _userRepository;
        private readonly ICat_categoria_productosRepository _cat_CategoriaREpository;
        private readonly Icat_productos_serviciosRepository _productos;

        public CatalogController(
             IMapper mapper,
             ILoggerManager logger,
             IUserRepository userRepository
           , ICat_categoria_productosRepository cat_CategoriaREpository
           , Icat_productos_serviciosRepository cat_Productos_Servicios)
        {
            _mapper = mapper;
            _logger = logger;
            _userRepository = userRepository;
            _cat_CategoriaREpository = cat_CategoriaREpository;
            _productos = cat_Productos_Servicios;
        }
        
        [HttpGet("Cat_Categorias", Name = "Cat_Categorias")]
        public ActionResult Cat_Categorias(int id_categoria)
        {
            try
            {
                IQueryable<CatCategoriaProducto> categorias = null;

                if (id_categoria != 0)
                {
                    categorias = _cat_CategoriaREpository.FindBy(x => x.Id == id_categoria);
                }
                else
                {
                    categorias = _cat_CategoriaREpository.GetAll();
                }

                if (categorias.Count() > 0)
                {
                    string pathimg = _userRepository.GetConfiguration("path_imagenes");
                    foreach (CatCategoriaProducto element in categorias)
                    {
                        element.Imagen = pathimg + element.Imagen;
                        element.ImagenSeleccion = pathimg + element.ImagenSeleccion;
                    }
                }

                return StatusCode(202, new
                {
                    Success = true,
                    Result = categorias,
                    Message = ""
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, new { Success = false, Result = 0, Message = $"Internal server error {ex.Message}" });
            }
        }

        [HttpGet("productos_by_Cat", Name = "productos_by_Cat")]
        public ActionResult productos_by_Cat(int id_categoria)
        {
            try
            {
                //var categorias = _cat_CategoriaREpository.GetAll();
                IQueryable<CatProductosServicio> res_productos = null;
                if ( id_categoria != 0)
                {
                     res_productos = _productos.FindBy(x => x.IdCategoriaProducto == id_categoria && x.Activo == true);
                }
                else
                {
                     res_productos = _productos.GetAll();
                }
                
                if(res_productos.Count() > 0 )
                {
                    string pathimg = _userRepository.GetConfiguration("path_imagenes");
                    foreach (CatProductosServicio element in res_productos)
                    {
                        if(element.TipoImagenSeleccion == "imagen")
                        {
                            element.ImagenSeleccion = pathimg + element.ImagenSeleccion;
                        }
                       
                    }
                }
               
                return StatusCode(202, new
                {
                    Success = true,
                    Result = res_productos,
                    Message = ""
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, new { Success = false, Result = 0, Message = $"Internal server error {ex.Message}" });
            }
        }

        [HttpGet("productos_by_Cat_Date", Name = "productos_by_Cat_Date")]
        public ActionResult productos_by_Cat_Date(int id_categoria, DateTime fecha_inicio)
        {
            try
            {
                //var categorias = _cat_CategoriaREpository.GetAll();
                ICollection res_productos = res_productos = _productos.productos_by_cateid_date(id_categoria, fecha_inicio);
 
                return StatusCode(202, new
                {
                    Success = true,
                    Result = res_productos,
                    Message = ""
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, new { Success = false, Result = 0, Message = $"Internal server error {ex.Message}" });
            }
        }

        [HttpGet("productos_by_id", Name = "productos_by_id")]
        public ActionResult productos_by_id(int id)
        {
            try
            {
                //var categorias = _cat_CategoriaREpository.GetAll();
                IQueryable<CatProductosServicio> res_productos = null;
                if (id != 0)
                {
                    res_productos = _productos.FindBy(x => x.Id == id);
                }
                else
                {
                    res_productos = _productos.GetAll();
                }

                if (res_productos.Count() > 0)
                {
                    string pathimg = _userRepository.GetConfiguration("path_imagenes");
                    foreach (CatProductosServicio element in res_productos)
                    {
                        element.ImagenSeleccion = pathimg + element.ImagenSeleccion;
                    }
                }

                return StatusCode(202, new
                {
                    Success = true,
                    Result = res_productos,
                    Message = ""
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, new { Success = false, Result = 0, Message = $"Internal server error {ex.Message}" });
            }
        }

        [HttpPost("productos_by_id_navigate", Name = "productos_by_id_navigate")]
        public ActionResult productos_by_id_navigate(int id)
        {
            try
            {
                
                var  res_productos = _productos.productos_by_id_navigate(id);
                return StatusCode(202, new
                {
                    Success = true,
                    Result = res_productos,
                    Message = ""
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, new { Success = false, Result = 0, Message = $"Internal server error {ex.Message}" });
            }
        }

        [HttpPost("productos_by_cateid_navigate", Name = "productos_by_cateid_navigate")]
        public ActionResult productos_by_cateid_navigate(int id_cat)
        {
            try
            {
                var res_productos = _productos.productos_by_cateid_navigate(id_cat);
                return StatusCode(202, new
                {
                    Success = true,
                    Result = res_productos,
                    Message = ""
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: { ex.ToString() }");
                return StatusCode(500, new { Success = false, Result = 0, Message = $"Internal server error {ex.Message}" });
            }
        }

        //[HttpPost("AddUpdateService", Name = "AddUpdateService")]
        //public async Task<ActionResult<ApiResponse<Evento>>> AddUpdateService(EventoDto _evento)
        //{
        //    var response = new ApiResponse<Evento>();

        //    try
        //    {
        //        // _evento.FechaCreacion = DateTime.Now;
        //        // _evento.FechaPago = DateTime.Now();
        //        var Sevento = _eventoRepository.Add(_mapper.Map<Evento>(_evento));
        //        StreamReader reader = new StreamReader(Path.GetFullPath("TemplateMail/Emaile.html"));
        //        string pathimg = _userRepository.GetConfiguration("path_imagenes");
        //        string body = string.Empty;
        //        body = reader.ReadToEnd();
        //        body = body.Replace("{user}", _evento.NombreContratane);
        //        body = body.Replace("{username}", $"{_evento.NombreContratane}");
        //        body = body.Replace("{path_imagenes}", pathimg);
        //        body = body.Replace("{pass}", _evento.FechaHoraInicio.ToString());

        //        _userRepository.SendMail(_evento.Correo, body, "Bienvenido a Conevento");

        //        response.Result = Sevento;
        //        response.Success = true;
        //        response.Message = "Evento creado con exíto";

        //    }
        //    catch (Exception ex)
        //    {
        //        response.Result = null;
        //        response.Success = false;
        //        response.Message = "Internal server error";
        //        _logger.LogError($"Something went wrong: { ex.ToString() }");
        //        return StatusCode(500, response);
        //    }

        //    return Ok(response);
        //}
    } 

}



