using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Notificaciones
{
    public class DTONotificacion
    {
        public int id { get; set; }
        public string mensaje { get; set; }
        public  DateTime fecha { get; set; }
        public int? cliente_id { get; set; }
        public bool? activa { get; set; }
    }
}
