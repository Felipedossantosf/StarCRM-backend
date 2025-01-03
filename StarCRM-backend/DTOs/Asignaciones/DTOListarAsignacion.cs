using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Asignaciones
{
    public class DTOListarAsignacion
    {
        public int id { get; set; }
        public int cliente_id { get; set; }
        public int? admin_id { get; set; }
        public int comun_id { get; set; }
        public DateTime fecha { get; set; }
        public string descripcion { get; set; }
        public string? estado { get; set; }
    }
}
