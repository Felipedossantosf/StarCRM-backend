using LogicaNegocio.Excepciones;
using LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Notificacion : IValidable
    {
        public int id { get; set; }
        public string mensaje { get; set; }
        public DateTime  fecha { get; set; }
        public int? cliente_id { get; set; }

        public void validar()
        {
            if (String.IsNullOrEmpty(mensaje))
                throw new NotificacionException("El mensaje no puede ser vacío.");
        }
    }
}
