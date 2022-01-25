﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.conevento.Entities
{
    public partial class CatProductosServicio
    {
        public CatProductosServicio()
        {
            ListaProductosEventos = new HashSet<ListaProductosEvento>();
        }

        public int Id { get; set; }
        public string Producto { get; set; }
        public string DescripcionCorta { get; set; }
        public string DescripcionLarga { get; set; }
        public int IdCategoriaProducto { get; set; }
        public decimal? PrecioPorUnidad { get; set; }
        public int? DiasBloqueoAntes { get; set; }
        public int? DiasBloqueoDespues { get; set; }
        public int IdCatTipoUnidad { get; set; }
        public int? MinimoProductos { get; set; }
        public string ImagenSeleccion { get; set; }
        public bool? Activo { get; set; }
        public bool? EspecificarTiempo { get; set; }
        public string TipoImagenSeleccion { get; set; }
        public int? MaximoProductos { get; set; }
        public string EspecificacionEspecial { get; set; }
        public string Sku { get; set; }
        public int? StockInicial { get; set; }

        public virtual CatTiposUnidad IdCatTipoUnidadNavigation { get; set; }
        public virtual CatCategoriaProducto IdCategoriaProductoNavigation { get; set; }
        public virtual ICollection<ListaProductosEvento> ListaProductosEventos { get; set; }
    }
}