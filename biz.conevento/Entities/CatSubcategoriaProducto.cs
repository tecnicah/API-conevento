﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.conevento.Entities
{
    public partial class CatSubcategoriaProducto
    {
        public CatSubcategoriaProducto()
        {
            CatProductosServicios = new HashSet<CatProductosServicio>();
        }

        public int Id { get; set; }
        public int? IdCategoria { get; set; }
        public string Subcategoria { get; set; }

        public virtual CatCategoriaProducto IdCategoriaNavigation { get; set; }
        public virtual ICollection<CatProductosServicio> CatProductosServicios { get; set; }
    }
}