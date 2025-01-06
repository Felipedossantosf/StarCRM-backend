using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Usuarios
{
    public class DTOUsuarioLogueado
    {
        public int? usuario_id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Rol { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
