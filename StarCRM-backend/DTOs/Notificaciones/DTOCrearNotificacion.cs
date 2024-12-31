using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Notificaciones
{
    public class DTOCrearNotificacion
    {
        public string mensaje { get; set; }
        public int? cliente_id { get; set; }
        public IEnumerable<int> usuariosId { get; set; }
    }
}
