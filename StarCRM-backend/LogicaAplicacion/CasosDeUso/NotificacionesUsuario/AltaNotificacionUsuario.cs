using AccesoDatos.Interfaces;
using DTOs.Notificaciones;
using LogicaAplicacion.Interfaces.NotificacionesUsuario;
using LogicaNegocio.Entidades;
using LogicaNegocio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.NotificacionesUsuario
{
    public class AltaNotificacionUsuario : IAltaNotificacionUsuario
    { 
        public IRepositorioNotificacionUsuario RepoNotificacionUsuario { get; set; }
        public IRepositorio<Notificacion> RepoNotificacion { get; set; }
        public AltaNotificacionUsuario(IRepositorioNotificacionUsuario repoNotificacionUsuario, IRepositorio<Notificacion> repoNotificacion)
        {
            RepoNotificacionUsuario = repoNotificacionUsuario;
            RepoNotificacion = repoNotificacion;
        }

        public DTONotificacion Alta(DTONotificacion dtoNotificacion, IEnumerable<int> idUsuarios)
        {
            Notificacion nuevaNotificacion = new Notificacion()
            {
                mensaje = dtoNotificacion.mensaje,
                fecha = dtoNotificacion.fecha,
                cliente_id = dtoNotificacion.cliente_id
            };

            RepoNotificacion.Add(nuevaNotificacion);

            IEnumerable<NotificacionUsuario> notificacionesUsuario = idUsuarios.Select(userId => new NotificacionUsuario()
            {
                usuario_id = userId,
                notificacion_id = nuevaNotificacion.id,
                activa = true,
                notificacion = nuevaNotificacion
            });

            RepoNotificacionUsuario.CrearNotificacionesUsuario(notificacionesUsuario);

            return dtoNotificacion;
        }
        
    }
}
