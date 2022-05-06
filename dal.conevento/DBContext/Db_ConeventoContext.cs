﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using biz.conevento.Entities;

namespace dal.conevento.DBContext
{
    public partial class Db_ConeventoContext : DbContext
    {
        public Db_ConeventoContext()
        {
        }

        public Db_ConeventoContext(DbContextOptions<Db_ConeventoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CatCategoriaProducto> CatCategoriaProductos { get; set; }
        public virtual DbSet<CatEstado> CatEstados { get; set; }
        public virtual DbSet<CatMunicipio> CatMunicipios { get; set; }
        public virtual DbSet<CatProductosServicio> CatProductosServicios { get; set; }
        public virtual DbSet<CatSubcategoriaProducto> CatSubcategoriaProductos { get; set; }
        public virtual DbSet<CatTiposUnidad> CatTiposUnidads { get; set; }
        public virtual DbSet<Cupone> Cupones { get; set; }
        public virtual DbSet<Evento> Eventos { get; set; }
        public virtual DbSet<ListaProductosEvento> ListaProductosEventos { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VariablesSistema> VariablesSistemas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatCategoriaProducto>(entity =>
            {
                entity.ToTable("Cat_categoria_productos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Categoria)
                    .HasColumnName("categoria")
                    .HasMaxLength(50);

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(350);

                entity.Property(e => e.Imagen).HasColumnName("imagen");

                entity.Property(e => e.ImagenSeleccion)
                    .HasColumnName("imagen_seleccion")
                    .HasMaxLength(350);
            });

            modelBuilder.Entity<CatEstado>(entity =>
            {
                entity.ToTable("Cat_estados");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasMaxLength(100)
                    .IsFixedLength();
            });

            modelBuilder.Entity<CatMunicipio>(entity =>
            {
                entity.ToTable("Cat_Municipios");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.IdCatEstado).HasColumnName("id_cat_estado");

                entity.Property(e => e.Municipio)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.HasOne(d => d.IdCatEstadoNavigation)
                    .WithMany(p => p.CatMunicipios)
                    .HasForeignKey(d => d.IdCatEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cat_Municipios_Cat_estados");
            });

            modelBuilder.Entity<CatProductosServicio>(entity =>
            {
                entity.ToTable("cat_productos_servicios");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.DescripcionCorta)
                    .HasColumnName("descripcion_corta")
                    .HasMaxLength(1000);

                entity.Property(e => e.DescripcionLarga)
                    .HasColumnName("descripcion_larga")
                    .HasMaxLength(4000);

                entity.Property(e => e.DiasBloqueoAntes).HasColumnName("dias_bloqueo_antes");

                entity.Property(e => e.DiasBloqueoDespues).HasColumnName("dias_bloqueo_despues");

                entity.Property(e => e.EspecificacionEspecial)
                    .HasColumnName("especificacion_especial")
                    .HasMaxLength(50);

                entity.Property(e => e.EspecificarTiempo).HasColumnName("especificar_tiempo");

                entity.Property(e => e.IdCatTipoUnidad).HasColumnName("id_cat_tipo_unidad");

                entity.Property(e => e.IdCategoriaProducto).HasColumnName("id_categoria_producto");

                entity.Property(e => e.IdSubcategoriaProductos).HasColumnName("id_subcategoria_productos");

                entity.Property(e => e.ImagenSeleccion)
                    .HasColumnName("imagen_seleccion")
                    .HasMaxLength(500);

                entity.Property(e => e.MaximoProductos).HasColumnName("maximo_productos");

                entity.Property(e => e.MinimoProductos).HasColumnName("minimo_productos");

                entity.Property(e => e.PrecioPorUnidad)
                    .HasColumnName("precio_por_unidad")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Producto)
                    .IsRequired()
                    .HasColumnName("producto")
                    .HasMaxLength(100);

                entity.Property(e => e.Sku)
                    .HasColumnName("SKU")
                    .HasMaxLength(50);

                entity.Property(e => e.StockInicial).HasColumnName("stock_inicial");

                entity.Property(e => e.TipoImagenSeleccion)
                    .HasColumnName("tipo_imagen_seleccion")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCatTipoUnidadNavigation)
                    .WithMany(p => p.CatProductosServicios)
                    .HasForeignKey(d => d.IdCatTipoUnidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cat_productos_servicios_Cat_Tipos_unidad1");

                entity.HasOne(d => d.IdCategoriaProductoNavigation)
                    .WithMany(p => p.CatProductosServicios)
                    .HasForeignKey(d => d.IdCategoriaProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cat_productos_servicios_Cat_categoria_productos");

                entity.HasOne(d => d.IdSubcategoriaProductosNavigation)
                    .WithMany(p => p.CatProductosServicios)
                    .HasForeignKey(d => d.IdSubcategoriaProductos)
                    .HasConstraintName("FK_cat_productos_servicios_Cat_subcategoria_productos");
            });

            modelBuilder.Entity<CatSubcategoriaProducto>(entity =>
            {
                entity.ToTable("Cat_subcategoria_productos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");

                entity.Property(e => e.Subcategoria)
                    .HasColumnName("subcategoria")
                    .HasMaxLength(500);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.CatSubcategoriaProductos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK_Cat_subcategoria_productos_Cat_categoria_productos");
            });

            modelBuilder.Entity<CatTiposUnidad>(entity =>
            {
                entity.ToTable("Cat_Tipos_unidad");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.TipoUnidad)
                    .HasColumnName("tipo_unidad")
                    .HasMaxLength(100)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Cupone>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Estatus).HasColumnName("estatus");

                entity.Property(e => e.FechaFinal)
                    .HasColumnName("fecha_final")
                    .HasColumnType("date");

                entity.Property(e => e.FechaInicial)
                    .HasColumnName("fecha_inicial")
                    .HasColumnType("date");

                entity.Property(e => e.MontoPesos)
                    .HasColumnName("monto_pesos")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MontoPorcentaje).HasColumnName("monto_porcentaje");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroCupones).HasColumnName("numero_cupones");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CalleNumero)
                    .IsRequired()
                    .HasColumnName("calle_numero")
                    .HasMaxLength(500);

                entity.Property(e => e.ClaveSeguimientoCarrito)
                    .HasColumnName("clave_seguimiento_Carrito")
                    .HasMaxLength(50);

                entity.Property(e => e.Colonia)
                    .IsRequired()
                    .HasColumnName("colonia")
                    .HasMaxLength(500);

                entity.Property(e => e.Correo)
                    .HasColumnName("correo")
                    .HasMaxLength(150);

                entity.Property(e => e.Cp)
                    .HasColumnName("cp")
                    .HasMaxLength(50);

                entity.Property(e => e.DetallesEvento)
                    .HasColumnName("detalles_evento")
                    .HasMaxLength(500);

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fecha_creacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaHoraFin)
                    .HasColumnName("fecha_hora_fin")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaHoraInicio)
                    .HasColumnName("fecha_hora_inicio")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaPago)
                    .HasColumnName("fecha_pago")
                    .HasColumnType("datetime");

                entity.Property(e => e.FormaPago)
                    .HasColumnName("forma_pago")
                    .HasMaxLength(100);

                entity.Property(e => e.GenteEsperada).HasColumnName("gente_esperada");

                entity.Property(e => e.IdCatMunicipio).HasColumnName("id_cat_municipio");

                entity.Property(e => e.IdCupon).HasColumnName("id_cupon");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.NombreContratane)
                    .HasColumnName("nombre_contratane")
                    .HasMaxLength(150);

                entity.Property(e => e.NombreEvento)
                    .HasColumnName("nombre_evento")
                    .HasMaxLength(50);

                entity.Property(e => e.Pagado).HasColumnName("pagado");

                entity.Property(e => e.ReferenciaPago)
                    .HasColumnName("referencia_pago")
                    .HasMaxLength(500);

                entity.Property(e => e.ReqFactura).HasColumnName("req_factura");

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasMaxLength(50);

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("numeric(10, 2)");

                entity.HasOne(d => d.IdCatMunicipioNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdCatMunicipio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Eventos_Cat_Municipios");

                entity.HasOne(d => d.IdCuponNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdCupon)
                    .HasConstraintName("FK_Eventos_Cupones");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_Eventos_users");
            });

            modelBuilder.Entity<ListaProductosEvento>(entity =>
            {
                entity.ToTable("Lista_productos_evento");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CantidadHoras).HasColumnName("cantidad_horas");

                entity.Property(e => e.CantidadUnidades).HasColumnName("cantidad_unidades");

                entity.Property(e => e.IdCatProducto).HasColumnName("id_cat_producto");

                entity.Property(e => e.IdEvento).HasColumnName("id_Evento");

                entity.HasOne(d => d.IdCatProductoNavigation)
                    .WithMany(p => p.ListaProductosEventos)
                    .HasForeignKey(d => d.IdCatProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lista_productos_evento_cat_productos_servicios");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.ListaProductosEventos)
                    .HasForeignKey(d => d.IdEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lista_productos_evento_Eventos");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellidos)
                    .HasColumnName("apellidos")
                    .HasMaxLength(100);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasColumnName("correo")
                    .HasMaxLength(200);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Estatus).HasColumnName("estatus");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fecha_creacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaEdicion)
                    .HasColumnName("fecha_edicion")
                    .HasColumnType("datetime");

                entity.Property(e => e.Municipio).HasColumnName("municipio");

                entity.Property(e => e.Nombres)
                    .HasColumnName("nombres")
                    .HasMaxLength(100);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnName("pass")
                    .HasMaxLength(50);

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<VariablesSistema>(entity =>
            {
                entity.ToTable("variables_sistema");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(4000);

                entity.Property(e => e.ValorVariable)
                    .IsRequired()
                    .HasColumnName("valor_variable")
                    .HasMaxLength(1000);

                entity.Property(e => e.Variable)
                    .IsRequired()
                    .HasColumnName("variable")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}