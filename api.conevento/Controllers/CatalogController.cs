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
using biz.conevento.Models.Events;
using biz.conevento.Repository.Catalogs;
using api.conevento.Models.Services;

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
        private readonly IcatSubcategoriaProductosRepository _subcategoriaProductosRepository;
        private readonly IEstadosRepository _estadosRepository;
        private readonly IMunicipiosRepository _municipiosRepository;

        public CatalogController(
             IMapper mapper,
             ILoggerManager logger,
             IUserRepository userRepository,
             ICat_categoria_productosRepository cat_CategoriaREpository,
             IcatSubcategoriaProductosRepository subcategoriaProductosRepository,
             Icat_productos_serviciosRepository cat_Productos_Servicios,
             IEstadosRepository estadosRepository,
             IMunicipiosRepository municipiosRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _userRepository = userRepository;
            _cat_CategoriaREpository = cat_CategoriaREpository;
            _subcategoriaProductosRepository = subcategoriaProductosRepository;
            _productos = cat_Productos_Servicios;
            _estadosRepository = estadosRepository;
            _municipiosRepository = municipiosRepository;
        }

        // Post Create a new CreateProductoServicios
        [HttpPost("CreateProductoServicios", Name = "CreateProductoServicios")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public ActionResult<ApiResponse<DtoCatProductosServicios>> CreateProductoServicios([FromBody] DtoCatProductosServicios dto)
        {
            var response = new ApiResponse<DtoCatProductosServicios>();
            try
            {

                if (_productos.IsBase64(dto.image)){
                    if (dto.image != null)
                    {
                        dto.ImagenSeleccion = _productos.UploadImageBase64(dto.image);
                    }
                }

                CatProductosServicio _r = _productos.Add(_mapper.Map<CatProductosServicio>(dto));
               

                response.Success = true;
                response.Message = "Success";
                //response.Result = _r;
                //response.Result = _productos.Add(_mapper.Map<CatProductosServicio>(dto));
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = ex.ToString();
                _logger.LogError($"Something went wrong: { ex.Message.ToString() }");
                return StatusCode(500, response);
            }
            return StatusCode(201, response);
        }

        // Put Update a CreateProductoServicios
        [HttpPut("UpdateProductoServicios", Name = "UpdateProductoServicios")]
        [ServiceFilterAttribute(typeof(ValidationFilterAttribute))]
        public ActionResult<ApiResponse<DtoCatProductosServicios>> UpdateProductoServicios([FromBody] DtoCatProductosServicios dto)
        {
            var response = new ApiResponse<DtoCatProductosServicios>();
            try
            {
                if (_productos.IsBase64(dto.image))
                {
                    if (dto.image != null)
                    {
                        dto.ImagenSeleccion = _productos.UploadImageBase64(dto.image);
                    }
                }

                CatProductosServicio _r = _productos.Update(_mapper.Map<CatProductosServicio>(dto), dto.Id);

                response.Success = true;
                response.Message = "Success";
                //response.Result = _productos.Update(_mapper.Map<CatProductosServicio>(dto), dto.Id);
            }
            catch (Exception ex)
            {
                response.Result = null;
                response.Success = false;
                response.Message = ex.ToString();
                _logger.LogError($"Something went wrong: { ex.Message.ToString() }");
                return StatusCode(500, response);
            }
            return StatusCode(201, response);
        }

        [HttpGet("Get_Cat_Categorias", Name = "Get_Cat_Categorias")]
        public ActionResult Get_Cat_Categorias()
        {
            try
            {
                var categorias = _cat_CategoriaREpository.GetAll();

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

        [HttpGet("Get_Cat_SubCategorias", Name = "Get_Cat_SubCategorias")]
        public ActionResult Get_Cat_SubCategorias(int id)
        {
            try
            {
                var categorias = _subcategoriaProductosRepository.FindBy(x => x.IdCategoria == id).ToList();

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

        [HttpGet("Get_Cat_CategoriasById", Name = "Get_Cat_CategoriasById")]
        public ActionResult Get_Cat_CategoriasById(int id_categoria)
        {
            try
            {
                //var categorias = _cat_CategoriaREpository.GetAll();
                IQueryable<CatProductosServicio> res_productos = null;

                res_productos = _productos.FindBy(x => x.Id == id_categoria);

                if (res_productos.Count() > 0)
                {
                    string pathimg = _userRepository.GetConfiguration("path_imagenes");
                    foreach (CatProductosServicio element in res_productos)
                    {
                        if (element.TipoImagenSeleccion == "imagen")
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

        [HttpPost("ActiveService", Name = "ActiveService")]
        public bool ActiveService(int id, bool active)
        {
            bool result = false;
            try
            {
                result = _productos.active_service(id, active);
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
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
                     res_productos = _productos.FindBy(x => x.IdCategoriaProducto == id_categoria && x.Activo == true && x.StockInicial > 0);
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

        [HttpPost("Productos_by_id_date", Name = "Productos_by_id_date")]
        public ActionResult Productos_by_id_date(dtolista _dtolista)
        {
            if (_dtolista is null)
            {
                throw new ArgumentNullException(nameof(_dtolista));
            }

            try
            {
                //var categorias = _cat_CategoriaREpository.GetAll();
                List<dtodispo> res_productos = res_productos = _productos.Productos_by_id_date(_dtolista);

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

        [HttpGet("Get_Estados", Name = "Get_Estados")]
        public ActionResult Get_Estados()
        {
            try
            {
                var categorias = _estadosRepository.GetAll();

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

        [HttpGet("Get_MunicipioByEstadoId", Name = "Get_MunicipioByEstadoId")]
        public ActionResult Get_MunicipioByEstadoId(int IdEstado)
        {
            try
            {
                var categorias = _municipiosRepository.FindBy(x => x.IdCatEstado == IdEstado);

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



