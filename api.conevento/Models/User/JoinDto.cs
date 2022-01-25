using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.conevento.Models.User
{
    public class JoinDto
    {

        public JoinDto()
        {

        }

        public string nombres { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string medio { get; set; }
        public string servicios { get; set; }
    }
}
