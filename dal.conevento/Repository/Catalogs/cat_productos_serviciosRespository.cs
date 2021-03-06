using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biz.conevento.Entities;
using biz.conevento.Models.Email;
using biz.conevento.Repository;
using biz.conevento.Servicies;
using dal.conevento.DBContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace dal.conevento.Repository
{
    public class cat_productos_serviciosRespository : GenericRepository<CatProductosServicio>, Icat_productos_serviciosRepository
    {

        private readonly IUserRepository _userRepository;
        private string _pathimg;

        public cat_productos_serviciosRespository(Db_ConeventoContext context, IUserRepository userRepository) : base(context)
        {
            _context = context;
            _userRepository = userRepository;
            _pathimg = _userRepository.GetConfiguration("path_imagenes");
        }



        public ICollection productos_by_id_navigate(int id)
        {

            var resultado = _context.CatProductosServicios.Select(x => new
            {
                x.Id,
                x.Producto,
                x.DescripcionCorta,
                descripcionCorta_lim = x.DescripcionCorta.Substring(0, 30) + "...",
                x.DescripcionLarga,
                x.IdCategoriaProducto,
                x.IdCategoriaProductoNavigation.Categoria,
                x.PrecioPorUnidad,
                x.DiasBloqueoAntes,
                x.DiasBloqueoDespues,
                x.IdCatTipoUnidad,
                x.IdCatTipoUnidadNavigation.TipoUnidad,
                x.MinimoProductos,
                ImagenSeleccion = _pathimg + x.ImagenSeleccion,
                x.Activo,
                x.EspecificarTiempo,
                x.TipoImagenSeleccion,
                x.MaximoProductos,
                x.EspecificacionEspecial,
                x.StockInicial,
                sku = x.Sku == null ? "Sku no cargado" : x.Sku,
                eventos = _context.ListaProductosEventos.Select(e => new
                {
                    e.Id,
                    e.CantidadHoras,
                    e.CantidadUnidades,
                    e.IdEventoNavigation,
                    e.IdCatProducto,
                    e.IdEventoNavigation.NombreContratane,
                    e.IdEventoNavigation.FechaHoraInicio,
                    e.IdEventoNavigation.FechaHoraFin,
                    FechaHoraInicio_add = e.IdEventoNavigation.FechaHoraInicio.AddDays(x.DiasBloqueoAntes.Value),
                    FechaHoraFin_add = e.IdEventoNavigation.FechaHoraFin.Value.AddDays(x.DiasBloqueoDespues.Value),
                    e.IdEventoNavigation.Correo,
                    e.IdEventoNavigation.CalleNumero,
                    e.IdEventoNavigation.Colonia,
                    e.IdEventoNavigation.Cp,
                    e.IdEventoNavigation.IdUsuario,
                    e.IdEventoNavigation.Telefono,
                    e.IdEventoNavigation.IdCatMunicipioNavigation.Municipio,
                    e.IdEventoNavigation.IdCatMunicipio,
                    total_servicios = e.IdEventoNavigation.ListaProductosEventos.Count(),
                    usuario = (e.IdEventoNavigation.IdUsuarioNavigation.Nombres == null ? "Sin" : e.IdEventoNavigation.IdUsuarioNavigation.Nombres) + " " + (e.IdEventoNavigation.IdUsuarioNavigation.Apellidos == null ? "Usuario" : e.IdEventoNavigation.IdUsuarioNavigation.Apellidos),
                    total = String.Format("{0:0.00#}", e.IdEventoNavigation.Total)
                }).Where(e => e.IdCatProducto == x.Id).ToList(),
            }).Where(x => x.Id == (id == 0 ? x.Id : id)).ToList();


            return resultado;

        }

        public ICollection productos_by_cateid_navigate(int id_cat)
        {

            var resultado = _context.CatProductosServicios.Select(x => new
            {
                x.Id,
                x.Producto,
                x.DescripcionCorta,
                descripcionCorta_lim = x.DescripcionCorta.Substring(0, 30) + "...",
                x.DescripcionLarga,
                x.IdCategoriaProducto,
                x.IdCategoriaProductoNavigation.Categoria,
                x.PrecioPorUnidad,
                x.DiasBloqueoAntes,
                x.DiasBloqueoDespues,
                x.IdCatTipoUnidad,
                x.IdCatTipoUnidadNavigation.TipoUnidad,
                x.MinimoProductos,
                ImagenSeleccion = _pathimg + x.ImagenSeleccion,
                x.Activo,
                x.EspecificarTiempo,
                x.TipoImagenSeleccion,
                x.MaximoProductos,
                x.EspecificacionEspecial,
                sku = x.Sku == null ? "Sku no cargado" : x.Sku,
                eventos = _context.ListaProductosEventos.Select(e => new
                {
                    e.Id,
                    e.CantidadHoras,
                    e.CantidadUnidades,
                    e.IdEventoNavigation,
                    e.IdCatProducto,
                    e.IdEventoNavigation.NombreContratane,
                    e.IdEventoNavigation.FechaHoraInicio,
                    e.IdEventoNavigation.FechaHoraFin,
                    FechaHoraInicio_add = e.IdEventoNavigation.FechaHoraInicio.AddDays(x.DiasBloqueoAntes.Value),
                    FechaHoraFin_add = e.IdEventoNavigation.FechaHoraFin.Value.AddDays(x.DiasBloqueoDespues.Value),
                    e.IdEventoNavigation.Correo,
                    e.IdEventoNavigation.CalleNumero,
                    e.IdEventoNavigation.Colonia,
                    e.IdEventoNavigation.Cp,
                    e.IdEventoNavigation.IdUsuario,
                    e.IdEventoNavigation.Telefono,
                    e.IdEventoNavigation.IdCatMunicipioNavigation.Municipio,
                    e.IdEventoNavigation.IdCatMunicipio,
                    total_servicios = e.IdEventoNavigation.ListaProductosEventos.Count(),
                    usuario = (e.IdEventoNavigation.IdUsuarioNavigation.Nombres == null ? "Sin" : e.IdEventoNavigation.IdUsuarioNavigation.Nombres) + " " + (e.IdEventoNavigation.IdUsuarioNavigation.Apellidos == null ? "Usuario" : e.IdEventoNavigation.IdUsuarioNavigation.Apellidos),
                    total = String.Format("{0:0.00#}", e.IdEventoNavigation.Total)
                }).Where(e => e.IdCatProducto == x.Id).ToList(),
            }).Where(x => x.IdCategoriaProducto == (id_cat == 0 ? x.IdCategoriaProducto : id_cat)).ToList();


            return resultado;

        }

        public int calcula_totales(int id_prod, DateTime fecha_inicio)
        {
            var eventos = _context.ListaProductosEventos.Where(e => e.IdCatProducto == id_prod);//&& fecha_inicio.Date == e.IdEventoNavigation.FechaHoraInicio.Date);

            var cant = 0;
            foreach(ListaProductosEvento x in eventos)
            {
                cant += x.CantidadUnidades;
            }

            return cant;
        }

        public ICollection productos_by_cateid_date(int id_cat, DateTime fecha_inicio)
        {
           

            var resultado = _context.CatProductosServicios.Select(x => new
            {
                x.Id,
                x.Producto,
                x.DescripcionCorta,
                descripcionCorta_lim = x.DescripcionCorta.Substring(0, 30) + "...",
                x.DescripcionLarga,
                x.IdCategoriaProducto,
                x.IdCategoriaProductoNavigation.Categoria,
                x.PrecioPorUnidad,
                x.DiasBloqueoAntes,
                x.DiasBloqueoDespues,
                x.IdCatTipoUnidad,
                x.IdCatTipoUnidadNavigation.TipoUnidad,
                x.MinimoProductos,
                ImagenSeleccion = _pathimg + x.ImagenSeleccion,
                x.Activo,
                x.EspecificarTiempo,
                x.TipoImagenSeleccion,
                x.MaximoProductos,
                x.EspecificacionEspecial,
                sku = x.Sku == null ? "Sku no cargado" : x.Sku,
               // ocupados = calcula_totales(id_cat,fecha_inicio),
                _ocupados = _context.ListaProductosEventos.Where(e => e.IdCatProducto == x.Id 
                                                                 && fecha_inicio.Date == e.IdEventoNavigation.FechaHoraInicio.Date).Sum(j =>j.CantidadUnidades)

            }).Where(x => x.IdCategoriaProducto == (id_cat == 0 ? x.IdCategoriaProducto : id_cat
                       )).ToList();
            
            
            return resultado;
        }

    }
}
