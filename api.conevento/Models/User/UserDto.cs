using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.conevento.Models.User
{
    public class UserDto
    {
        public UserDto()
        {
            
        }

        public int Id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public int estado { get; set; }
        public int municipio { get; set; }
        public string pass { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_edicion { get; set; }

    }
}
