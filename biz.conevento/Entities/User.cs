﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace biz.conevento.Entities
{
    public partial class User
    {
        public User()
        {
            Eventos = new HashSet<Evento>();
        }

        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public int? Estado { get; set; }
        public int? Municipio { get; set; }
        public bool? Estatus { get; set; }
        public string Pass { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; }
    }
}