using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.conevento.Models.Events
{
    public class dtolista
    {

        public dtolista()
        {
             
        }

        public List<dtoproductos> ListaProductosEventos { get; set; }
        public DateTime Fecha { get; set; }


    }

    public class dtoproductos
    {
        public dtoproductos() { }

        public int id  { get; set; }
        public int idEvento  { get; set; }
        public int idCatProducto { get; set; }
        public int cantidadUnidades { get; set; }
        public int cantidadHoras  { get; set; }
        public int idCategoria  { get; set; } 
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public bool  especificarTiempo { get; set; }

    }
}
