﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.conevento.Entities
{
    public partial class CatMunicipio
    {
        public CatMunicipio()
        {
            Eventos = new HashSet<Evento>();
        }

        public int Id { get; set; }
        public string Municipio { get; set; }
        public int IdCatEstado { get; set; }
        public string Activo { get; set; }

        public virtual CatEstado IdCatEstadoNavigation { get; set; }
        public virtual ICollection<Evento> Eventos { get; set; }
    }
}