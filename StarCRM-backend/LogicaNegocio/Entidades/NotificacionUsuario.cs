using LogicaNegocio.Excepciones;
using LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class NotificacionUsuario : IValidable
    {
        public int id { get; set; }
        public int usuario_id { get; set; }
        public int notificacion_id { get; set; }
        public Notificacion notificacion { get; set; }
        public  bool activa { get; set; }

        public void validar()
        {
            if (usuario_id == null || notificacion_id == null)
                throw new NotificacionUsuarioException("usuario o notificacion null.");
        }
    }
}
