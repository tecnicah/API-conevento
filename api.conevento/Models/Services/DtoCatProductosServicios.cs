using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.conevento.Models.Services
{
    public class DtoCatProductosServicios
    {
       public  DtoCatProductosServicios()
        {

        }

        public int Id { get; set; }
        public string Producto { get; set; }
        public string DescripcionCorta { get; set; }
        public string DescripcionLarga { get; set; }
        public int IdCategoriaProducto { get; set; }
        public int? IdSubcategoriaProductos { get; set; }
        public decimal? PrecioPorUnidad { get; set; }
        public int? DiasBloqueoAntes { get; set; }
        public int? DiasBloqueoDespues { get; set; }
        public int? idCatTipoUnidad { get; set; }
        public int? MinimoProductos { get; set; }
        public string ImagenSeleccion { get; set; }
        public bool? Activo { get; set; }
        public bool? EspecificarTiempo { get; set; }
        public string TipoImagenSeleccion { get; set; }
        public int? MaximoProductos { get; set; }
        public string EspecificacionEspecial { get; set; }
        public string Sku { get; set; }
        public int? StockInicial { get; set; }
        public string? image { get; set; }

    }
}
