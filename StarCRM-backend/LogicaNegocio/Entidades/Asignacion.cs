using LogicaNegocio.Excepciones;
using LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Asignacion : IValidable
    {
        public int id { get; set; }
        public int cliente_id { get; set; }
        public int? admin_id { get; set; }
        public int comun_id { get; set; }
        public DateTime fecha { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; } // aprobada, pendiente, rechazada


        public void validar()
        {
            if (estado.ToLower() != "aprobada" && estado.ToLower() != "pendiente" && estado.ToLower() != "rechazada")
                throw new AsignacionException("Estado de asignación incorrecto.");
            if (cliente_id == null)
                throw new AsignacionException("cliente_id no puede ser nulo");
        }
    }
}
