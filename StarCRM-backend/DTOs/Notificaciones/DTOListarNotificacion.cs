using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Notificaciones
{
    public class DTOListarNotificacion
    {
        public int notificacion_usuario_id { get; set; }
        public int notificacion_id { get; set; }
        public string mensaje { get; set; }
        public DateTime fecha { get; set; }
        public int? cliente_id { get; set; }
        public bool? activa { get; set; }
    }
}
