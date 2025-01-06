using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Actividades
{
    public class DTOListarActividad
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public string descripcion { get; set; }
        public int usuario_id { get; set; }
    }
}
