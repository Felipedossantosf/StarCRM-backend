using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Notificaciones
{
    public class DTONotificacionUsuario
    {
        public int id { get; set; }
        public int notificacion_id { get; set; }
        public int usuario_id { get; set; }
        public bool activa { get; set; }
    }
}
